﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	private Animator animator;
	private bool grounded;
	private Transform groundCheck;

	public float maxSpeed = 10f;
	public float jumpForce = 700;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		//aninator.SetTrigger("stand");
		groundCheck = transform.Find("groundCheck");
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));  
	    

		//float move = Input.GetAxis("Horizontal");
		animator.SetBool("grounded", grounded);
		//rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		animator.SetFloat("speed", Mathf.Abs(maxSpeed));
		if(grounded && Input.GetButton("Jump"))
			ApplyJump();

	}

	private void ApplyJump(){

		animator.SetTrigger("jump");
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce));
	}

}
