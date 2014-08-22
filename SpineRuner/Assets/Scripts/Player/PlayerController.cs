using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	//private Animator animator;
	private float speed;
    private bool grounded;
	private Transform groundCheck;
	public float maxSpeed = 10f;
	public float jumpForce = 700;
    SkeletonAnimation animation;
	void Start () {
		//animator = GetComponent<Animator>();
		groundCheck = transform.Find("groundCheck");
        animation = GetComponent<SkeletonAnimation>();
	}


	private void FixedUpdate()
	{
		Grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));  
		
		rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		//animator.SetFloat("speed", Mathf.Abs(maxSpeed));
		if(grounded && Input.GetButton("Jump"))
			ApplyJump();

	}

	private void ApplyJump(){

		//animator.SetTrigger("jump");
		rigidbody2D.AddForce(new Vector2(0,jumpForce));
	}
    public bool Grounded
    {  
        get { return grounded; }
        set 
        {
            if (grounded == value)
                return;
            grounded = value;
            if(grounded)
            {
                
                animation.state.SetAnimation(0, "run", true);
            }
            else
            {
                animation.state.ClearTrack(0);
                animation.state.SetAnimation(1, "jump", false);
            }
        }
    }


}
