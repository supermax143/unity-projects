using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	private Animator aninator;
	private float speed;
	private bool grounded;
	private Transform groundCheck;

	public float maxSpeed = 10f;
	public float jumpForce = 700;

	// Use this for initialization
	void Start () {
		aninator = GetComponent<Animator>();
		//aninator.SetTrigger("stand");
		groundCheck = transform.Find("groundCheck");
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));  
	
		Debug.Log(grounded);

		float move = Input.GetAxis("Horizontal");
		aninator.SetFloat("speed", Mathf.Abs(move));
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		if(grounded && Input.GetButton("Jump"))
			ApplyJump();
	}

	private void ApplyJump(){
		rigidbody2D.AddForce(new Vector2(0,jumpForce));
	}

}
