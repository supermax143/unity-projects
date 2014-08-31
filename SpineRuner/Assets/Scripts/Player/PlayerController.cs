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
    private bool grounded;

    float timeToWait = 0f;


    void Start () {
        groundCheck = transform.Find("bottom_collider");
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        colliders = GetComponentsInChildren<ColiderScript>();
        foreach (ColiderScript c in colliders)
        {
            c.collisionEnterEvent += OnCollisionEnterEvent;
        }
	}

    


	private void Update()
	{
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));
        if(Waiting)
            return;
        switch (state)
        { 
            case State.Run:
                OnStateRun();
                break;
            case State.Jump:
                OnStateJump();
                break;
            case State.Hurt:
                OnStateHurt();
                break;
        }        
	}

    private void OnStateHurt()
    {
        if (grounded)
            SetState(State.Run);
    }

    private void OnStateJump()
    {
        if (grounded)
            SetState(State.Run);
    }

    private void OnStateRun()
    {
        rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
        if(grounded && InputManager.Instance.IsJumpPressed)
            SetState(State.Jump);
    }

    private void OnCollisionEnterEvent(string sender, Collider2D collider)
    {
        if (Waiting)
            return;
        if(collider.gameObject.tag == "Enemy")
        {
            Debug.Log(sender);
            ColiderScript coliderScript = collider.gameObject.GetComponent<ColiderScript>();
            if (coliderScript.colliderName == "head")
            {
                ApplyJump();
                SetWaitTime(.1f);
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
        if (state == value||Waiting)
            return;
        switch(value)
        {
            case State.Run:
                skeletonAnimation.state.SetAnimation(0, "run", true);
                break;
            case State.Jump:
                skeletonAnimation.state.ClearTrack(0);
                skeletonAnimation.state.SetAnimation(1, "jump", false);
                ApplyJump();
                break;
            case State.Hurt:
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(2, "hurt", false);
                rigidbody2D.velocity = new Vector2(0, 0);
                rigidbody2D.AddForce(new Vector2(-700, 2000));
                SetWaitTime(0.1f);   
                break;
        }
        state = value;
    }


    public bool Waiting
    {
        get {return timeToWait>TimerManager.Instance.CurrentTime;}
    }

    void SetWaitTime(float value)
    {
        timeToWait = TimerManager.Instance.CurrentTime + value;
    }

	private void ApplyJump(){
		rigidbody2D.AddForce(new Vector2(0,jumpForce));
	}

}
