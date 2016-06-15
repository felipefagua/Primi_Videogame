using System;
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public Action AttackAction { get; set; }

    private Transform _myTransform;

    [SerializeField]
    private float _collideDistance;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private KeyCode _moveUpKey;

    [SerializeField]
    private KeyCode _moveDownKey;

    [SerializeField]
    private KeyCode _moveLeftKey;

    [SerializeField]
    private KeyCode _moveRightKey;

    [SerializeField]
    private KeyCode _attackKey;

    [SerializeField] 
    private KeyCode _jumpKey;

    private JumpController _jumpController;
    
    private void Start() {
        _myTransform = transform;
        _jumpController = GetComponent<JumpController>();
    }

    private void Update () {
		CheckInput ();
		//CheckPS4Input ();
	}

	void CheckPS4Input () {
		CheckMovementPS4Input ();
		CheckAttackPS4Input ();
	}

	void CheckMovementPS4Input () {
		float xInput = Input.GetAxis("Horizontal");
		float yInput = Input.GetAxis("Vertical");
		Vector3 movementVector = new Vector3(xInput, yInput);
		if (movementVector.magnitude < 0.3f)
			movementVector = new Vector3 ();

		TryToMoveCharacter (movementVector);
	}

	void CheckAttackPS4Input () {
		if (Input.GetButtonDown("Fire1") && 
			AttackAction != null)
			AttackAction();
	}

    private void CheckInput() {
        CheckMovementInput();
        CheckAttackInput();
    }

    private void CheckMovementInput() {
        Vector3 movementVector = new Vector3();

        if (Input.GetKey(_moveUpKey))
            movementVector += new Vector3(0, 1);
        if (Input.GetKey(_moveDownKey))
            movementVector += new Vector3(0, -1);
        if (Input.GetKey(_moveLeftKey))
            movementVector += new Vector3(-1, 0);
        if (Input.GetKey(_moveRightKey))
            movementVector += new Vector3(1, 0);
        if (Input.GetKey(_jumpKey))
            TryToJump();

        if (_jumpController.IsJumping) {
            movementVector.y = _jumpController.JumpVector.y;
            MoveCharacterDuringJump(movementVector);
        }
        else
            TryToMoveCharacter(movementVector);

    }

    private void MoveCharacterDuringJump(Vector3 movementVector) {
        Vector3 jumpStep = movementVector * _movementSpeed * Time.deltaTime;
        _myTransform.position += jumpStep;
        if (_jumpController.IsJumping && _jumpController.IsGoingUp &&
            _myTransform.position.y < _jumpController.StartJumpPosition.y) {
            Vector3 newPostion = _myTransform.position;
            newPostion.y = _jumpController.StartJumpPosition.y;
            _myTransform.position = newPostion;
        } 
            
    }

    private void TryToJump() {
        if (!_jumpController.IsJumping)
            _jumpController.StartJump();
    }

    private void TryToMoveCharacter (Vector3 movementVector) {
		Vector3 step = movementVector.normalized * _movementSpeed * Time.deltaTime;
		Vector3 candidatePosition = _myTransform.position + step;

		RaycastHit2D[] hits = Physics2D.RaycastAll(_myTransform.position, step, _collideDistance);

		if (!HitAnObstacle(hits)) {
			_myTransform.position = candidatePosition;
			if (movementVector.magnitude > 0) {
				GetComponent<Animator>().SetBool("isWalking", true);
				if (movementVector.x < 0) {
					Vector3 newScale = _myTransform.localScale;
					newScale.x = Mathf.Abs(newScale.x) * -1;
					_myTransform.localScale = newScale;
				}
				else {
					Vector3 newScale = _myTransform.localScale;
					newScale.x = Mathf.Abs(newScale.x);
					_myTransform.localScale = newScale;
				}
			}
			else
				GetComponent<Animator>().SetBool("isWalking", false);

		}		
	}

    private bool HitAnObstacle(RaycastHit2D[] hits) {
        foreach(RaycastHit2D hit in hits) {
            if (hit.collider.tag == "InvisibleObstacle" && 
                hit.collider.gameObject != gameObject)
                return true;
        }
        return false;
    }

    private void CheckAttackInput() {
        if (Input.GetKeyDown(_attackKey) && 
            AttackAction != null)
            AttackAction();
    }
}
