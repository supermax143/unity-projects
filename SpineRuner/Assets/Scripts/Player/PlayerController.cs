using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private float speed;
	private Transform groundCheck;
	public float maxSpeed = 10f;
	public float jumpForce = 700;
    SkeletonAnimation skeletonAnimation;
    ColiderScript[] colliders;

    enum State  { Run, Jump, Hurt};
    private State state = State.Run;

    void Start () {
        groundCheck = transform.Find("bottom_collider");
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        colliders = GetComponentsInChildren<ColiderScript>();
        foreach (ColiderScript c in colliders)
            c.collisionEvent += OnCollisionEvent; 
	}


	private void FixedUpdate()
	{
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground")))
            SetState(State.Run);
        else
            SetState(State.Jump);
        if (state == State.Run)
		    rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
        if (state == State.Run && Input.GetButton("Jump"))
			ApplyJump();

	}


    private void OnCollisionEvent(string sender, Collider2D collider)
    {
        if (collider.gameObject.tag == "Untagged")
            return;
        if(collider.gameObject.tag == "Enemy")
        {
            ColiderScript coliderScript = collider.gameObject.GetComponent<ColiderScript>();
            if (coliderScript.colliderName == "head")
            {
                ApplyJump();
                Debug.Log("kill bustard");
            }
            if (coliderScript.colliderName == "weapon")
            {
                SetState(State.Hurt);
                Debug.Log("Hurt");
            }

        }


    }

    private void SetState(State value)
    {
        if (state == value)
            return;
        switch(value)
        {
            case State.Run:
                skeletonAnimation.state.SetAnimation(0, "run", true);
                break;
            case State.Jump:
                skeletonAnimation.state.ClearTrack(0);
                skeletonAnimation.state.SetAnimation(1, "jump", false);
                break;
            case State.Hurt:
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(2, "hurt", false);
                rigidbody2D.velocity = new Vector2(0, 0);
                rigidbody2D.AddForce(new Vector2(-4, 5));
                break;
        }
        state = value;
    }


	private void ApplyJump(){

		rigidbody2D.AddForce(new Vector2(0,jumpForce));
	}

}
