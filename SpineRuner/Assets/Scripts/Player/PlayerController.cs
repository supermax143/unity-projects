using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	private float speed;
    private bool grounded;
	private Transform groundCheck;
	public float maxSpeed = 10f;
	public float jumpForce = 700;
    SkeletonAnimation skeletonAnimation;

    ColiderScript[] colliders;

	void Start () {
        groundCheck = transform.Find("bottom_collider");
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        colliders = GetComponentsInChildren<ColiderScript>();
        foreach (ColiderScript c in colliders)
            c.collisionEvent += OnCollisionEvent; 
	}


	private void FixedUpdate()
	{
		Grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));  
		rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		//animator.SetFloat("speed", Mathf.Abs(maxSpeed));
		if(grounded && Input.GetButton("Jump"))
			ApplyJump();

	}


    private void OnCollisionEvent(string sender, Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag);
        if(collider.gameObject.tag == "Enemy")
        {
            ColiderScript coliderScript = collider.gameObject.GetComponent<ColiderScript>();
            if (coliderScript.colliderName == "head")
                ApplyJump();
        }


    }

  


	private void ApplyJump(){

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
                
                skeletonAnimation.state.SetAnimation(0, "run", true);
            }
            else
            {
                skeletonAnimation.state.ClearTrack(0);
                skeletonAnimation.state.SetAnimation(1, "jump", false);
            }
        }
    }


}
