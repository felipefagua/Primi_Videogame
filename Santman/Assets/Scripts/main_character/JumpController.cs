using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour {
    
    [SerializeField]
    private bool _isJumping = false;

    public bool IsJumping {
        get {
            return _isJumping;
        }
    }

    [SerializeField]
    private bool _isGoingUp = false;

    public bool IsGoingUp {
        get { return _isGoingUp; }
    }

    [SerializeField]
    private float _maxJumpDistance;
    
    [SerializeField]
    private float _jumpSpeed;

    [SerializeField]
    private float _startJumpSpeed;

    [SerializeField]
    private float _damp;

    [SerializeField]
    private Vector3 _jumpVector;

    public Vector3 JumpVector {
        get {
            return _jumpVector;
        }
    }

    private Vector3 _startJumpPosition;

    public Vector3 StartJumpPosition {
        get { return _startJumpPosition; }
    }

    private Transform _myTransform;

    private void Start() {
        _myTransform = transform;
    }

    private void Update () {
	    if (_isJumping)    
            UpdateJump();
	}

    public void StartJump() {
        _isJumping = true;
        _isGoingUp = true;
        _jumpSpeed = _startJumpSpeed;
        _startJumpPosition = _myTransform.position;
    }

    public void UpdateJump() {
        if (_isGoingUp)
            UpdateRise();
        else
            UpdateFall();
    }

    private void UpdateRise() {
        UpdateJumpVector(1);
        if (_jumpSpeed <= 0)
            _isGoingUp = false;
    }

    private void UpdateFall() {
        UpdateJumpVector(-1);
        if (_myTransform.position.y <= _startJumpPosition.y) {
            _isJumping = false;
            Vector3 newPosition = _myTransform.position;
            newPosition.y = _startJumpPosition.y;
            _myTransform.position = newPosition;
        }
    }

    private void UpdateJumpVector(int direction) {
        _jumpVector = new Vector3();
        _jumpVector.y = _jumpSpeed * direction;
        _jumpSpeed += (direction*_damp*-1);
        if (_jumpSpeed >= _startJumpSpeed)
            _jumpSpeed = _startJumpSpeed;
    }

    public void EndJump() {
        _isJumping = false;
        _isGoingUp = false;
    }
}
