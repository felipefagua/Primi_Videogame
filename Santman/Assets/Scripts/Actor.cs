using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    public float horizontalMaxSpeed = 10;
    public float horizontalMovement;

    private Rigidbody2D myRigidbody2D;
    private Transform myTransform;
    private Animator myAnimator;
    private bool facingRight = true;

	// Use this for initialization
	void Start () {
	    InitExternalComponentReferences();
	}

    private void InitExternalComponentReferences() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
        myAnimator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
	void FixedUpdate () {
	    MoveHorizontally();
    }

    private void MoveHorizontally() {
        myAnimator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        myRigidbody2D.velocity = new Vector2(horizontalMovement * horizontalMaxSpeed, myRigidbody2D.velocity.y);
        CheckFlip();
    }

    public void SetHorizontalMovementTo(float movement) {
        horizontalMovement = movement;
    }

    private void CheckFlip() {
        if (horizontalMovement < 0 && facingRight)
            Flip();
        else if (horizontalMovement > 0 && !facingRight)
            Flip();
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 newLocalScale = myTransform.localScale;
        newLocalScale.x *= -1;
        myTransform.localScale = newLocalScale;
    }


}
