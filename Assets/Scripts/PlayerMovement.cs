using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 10f;
	public float jumpForce = 700f;
	public LayerMask BlockingLayer;
	public Transform groundCheck;
	private Animator animator;
	private Rigidbody2D rb2d;
	private bool grounded = true;
	private float groundRadius = 0.1f;
	private bool facingRight = true;
	private float JumpCooldown = 1f;
	private float nextJump = 0;
	

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.rotation = Quaternion.identity;

		float xDir = Input.GetAxis("Horizontal");
		float yDir = Input.GetAxis ("Vertical");

		if (xDir != 0) MovePlayer (xDir);
		else if(xDir == 0 && yDir == 0){
			animator.SetTrigger ("idle");	
		} 
			


		FaceDirection(xDir);		
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.UpArrow) && nextJump <= Time.time) {
			nextJump = Time.time + JumpCooldown;
			rb2d.AddForce (new Vector2 (0, jumpForce));
			animator.SetTrigger ("jump");
		}

	}

	void FaceDirection(float xDir){
		if(xDir>0 && !facingRight) Flip();
		else if(xDir<0 && facingRight) Flip();
	}	

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void MovePlayer(float xDir){
		animator.SetTrigger ("walk");
		float xSpeed = xDir * speed;
		rb2d.velocity = new Vector2(xSpeed, rb2d.velocity.y);	
	}
}
