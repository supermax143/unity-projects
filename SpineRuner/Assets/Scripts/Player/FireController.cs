using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour
{

	//private const int IDLE = 1;
	//private const int START_THROW = 2;
	//private const int BOOST_THROW = 3;
	//private const int THROW = 4;

    private enum FireState { IDLE, START_THROW, BOOST_THROW, THROW };

	//boost
	public float throwBoostTime = 1.5f;
	private float boostStarTime;
	//throw 
	public float minThrowVelocity = 22;
	public float throwVelocityAdd = 20;

	//weapon
	public GameObject axe;
	
	private Spine.Bone weaponPlace;

	private FireState fireState = FireState.IDLE;
    SkeletonAnimation skeletonAnimation;
		
	void Start ()
	{
		
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        weaponPlace = skeletonAnimation.skeleton.FindBone("axe");
       
	}



	void Update ()
	{
        
		switch(fireState)
		{
			case FireState.IDLE:
				OnStateIdle();
				break;
            case FireState.START_THROW:
				OnStateStartThrow();
				break;
            case FireState.BOOST_THROW:
				OnStateBoostThrow();
				break;
		}
	}

	void OnStateStartThrow()
	{
        GetComponent<PlayerController>().StateChanged += OnMainStateStateChanged;
	}

    void OnMainStateStateChanged(PlayerController.State state)
    {
        if (state == PlayerController.State.Hurt)
            fireState = FireState.IDLE;
    }


	
	void OnStateIdle()
	{
		if(InputManager.Instance.IsFirePressed)
			StartThrow(); 
	}

	private void StartThrow()
	{
        fireState = FireState.START_THROW;
        skeletonAnimation.state.SetAnimation(2, "throw_start",false);
        skeletonAnimation.state.End += SartThrowCompleteHandler;
	}

    private void SartThrowCompleteHandler(Spine.AnimationState state, int trackIndex)
    {
        if (trackIndex != 2)
            return;
        skeletonAnimation.state.End -= SartThrowCompleteHandler;
        skeletonAnimation.state.SetAnimation(3, "throw_boost", true);
        this.fireState = FireState.BOOST_THROW;
		boostStarTime = TimerManager.Instance.CurrentTime;
    }
	void OnStateBoostThrow()
	{
        if (!InputManager.Instance.IsFirePressed)
			ThrowWeapon();
	}

	public void ThrowWeapon()
	{
        skeletonAnimation.state.ClearTrack(3);
        skeletonAnimation.state.SetAnimation(4, "throw", false);
        skeletonAnimation.state.End += ThrowAnimationCompleteHandler;
        skeletonAnimation.state.Event += OnWeaponThrown;
        fireState = FireState.THROW;
    }

    void OnWeaponThrown(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        if (e.String != "throw")
            return;
        skeletonAnimation.state.Event -= OnWeaponThrown;

        float koef = BoostTime() / throwBoostTime;
        if (koef > 1)
            koef = 1;
        float xVel = minThrowVelocity + (throwVelocityAdd * koef);
        
        Vector3 axePosition = new Vector3(skeletonAnimation.transform.position.x + weaponPlace.worldX, skeletonAnimation.transform.position.y + weaponPlace.worldY);

        Rigidbody2D axeIstance = Instantiate(axe, axePosition, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;


        float ang = InputManager.Instance.FireAngle * Mathf.Deg2Rad;

        axeIstance.velocity = new Vector2(Mathf.Cos(ang) * xVel, Mathf.Sin(ang) * xVel);
        

        axeIstance.AddTorque(-400);
    }

    void ThrowAnimationCompleteHandler(Spine.AnimationState state, int trackIndex)
    {
        if (trackIndex != 4)
            return;
        skeletonAnimation.state.End -= ThrowAnimationCompleteHandler;
        this.fireState = FireState.IDLE;
    }

	

	float BoostTime()
	{
		return TimerManager.Instance.CurrentTime - boostStarTime;
	}

}
