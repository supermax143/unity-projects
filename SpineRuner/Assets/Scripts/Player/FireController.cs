using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour
{

	private const int IDLE = 1;
	private const int START_THROW = 2;
	private const int BOOST_THROW = 3;
	private const int THROW = 4;

	//boost
	public float throwBoostTime = 1.5f;
	private float boostStarTime;
	//throw 
	public float minThrowVelocityX = 22;
	public float minThrowVelocityY = 13;
	public float throwVelocityAddX = 20;
	public float throwVelocityAddY = 10;
	//weapon
	public Rigidbody2D axe;
	
	private bool fireButtonDown = false;
	//private Animator animator;
	private bool throwing = false;
	private Spine.Bone weaponPlace;

	private int state = IDLE;
    SkeletonAnimation animation;
		
	void Start ()
	{
		
        animation = GetComponent<SkeletonAnimation>();
        weaponPlace = animation.skeleton.FindBone("axe");
       
	}



	void Update ()
	{
        
		CheckButtons();
		switch(state)
		{
			case IDLE:
				OnStateIdle();
				break;
			case START_THROW:
				OnStateStartThrow();
				break;
			case BOOST_THROW:
				OnStateBoostThrow();
				break;
		}
	}

	void OnStateStartThrow()
	{

	}

	void CheckButtons()
	{
		if(Input.GetMouseButtonDown(0))
			fireButtonDown = true;
		else if(Input.GetMouseButtonUp(0))
			fireButtonDown = false;
	}

	void OnStateIdle()
	{
		//Debug.Log("state idle");
		if(fireButtonDown)
			StartThrow(); 
	}

	private void StartThrow()
	{
		state = START_THROW;
		//animator.SetTrigger("start_throw");
        animation.state.SetAnimation(2, "throw_start",false);
        animation.state.End += SartThrowCompleteHandler;
		throwing = true;
	}

    private void SartThrowCompleteHandler(Spine.AnimationState state, int trackIndex)
    {
        if (trackIndex != 2)
            return;
        Debug.Log(trackIndex);
        animation.state.End -= SartThrowCompleteHandler;
        animation.state.SetAnimation(3, "throw_boost", true);
		this.state = BOOST_THROW;
		boostStarTime = TimerManager.Instance.CurrentTime;
    }
	void OnStateBoostThrow()
	{
		//Debug.Log("state boost throw");
		if(!fireButtonDown)
			ThrowWeapon();
	}

	public void ThrowWeapon()
	{
        animation.state.ClearTrack(3);
        animation.state.SetAnimation(4, "throw", false);
        animation.state.End += ThrowAnimationCompleteHandler;
        animation.state.Event += OnWeaponThrown;
        state = THROW;
    }

    void OnWeaponThrown(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        if (e.String != "throw")
            return;
        animation.state.Event -= OnWeaponThrown;

        float koef = BoostTime() / throwBoostTime;
        if (koef > 1)
            koef = 1;
        float xVel = minThrowVelocityX + (throwVelocityAddX * koef);
        float yVel = minThrowVelocityY + (throwVelocityAddY * koef);
        Vector3 axePosition = new Vector3(animation.transform.position.x + weaponPlace.worldX, animation.transform.position.y + weaponPlace.worldY);

        Rigidbody2D axeIstance = Instantiate(axe, axePosition, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        axeIstance.velocity = new Vector2(xVel, yVel);
        axeIstance.AddTorque(-250);
    }

    void ThrowAnimationCompleteHandler(Spine.AnimationState state, int trackIndex)
    {
        if (trackIndex != 4)
            return;
        animation.state.End -= ThrowAnimationCompleteHandler;
		this.state = IDLE;
    }

	

	float BoostTime()
	{
		return TimerManager.Instance.CurrentTime - boostStarTime;
	}

}
