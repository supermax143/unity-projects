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
	private Animator animator;
	private bool throwing = false;
	private Transform weaponPlace;

	private int state = IDLE;


	void Start ()
	{
		animator =transform.root.gameObject.GetComponent<Animator>();
		Transform leftArm = transform.FindChild("ork_left_arm");
		weaponPlace = leftArm.FindChild("ork_weapon");
        GameObject aaa = GameObject.Find("asd"); 
	}

	void Update ()
	{
		//if(Input.GetButtonDown("Fire1"))

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

	void OnStateBoostThrow()
	{
		//Debug.Log("state boost throw");
		if(!fireButtonDown)
			ThrowWeapon();
	}

	private void StartThrow()
	{
		state = START_THROW;
		animator.SetTrigger("start_throw");
		throwing = true;
	}

	void StartBoostThrow()
	{
		state = BOOST_THROW;
		boostStarTime = TimerManager.Instance.CurrentTime;
	}

	void OnWeaponThrown()
	{
		float koef = BoostTime()/throwBoostTime;
		if(koef>1)
			koef = 1;
	    float xVel = minThrowVelocityX + (throwVelocityAddX*koef);
		float yVel = minThrowVelocityY + (throwVelocityAddY*koef);
		Rigidbody2D axeIstance = Instantiate(axe, weaponPlace.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		axeIstance.velocity = new Vector2(xVel, yVel);
		axeIstance.AddTorque(-250);
		state = IDLE;

	}
	public void ThrowWeapon()
	{
		animator.SetTrigger("throw");
	}

	float BoostTime()
	{
		return TimerManager.Instance.CurrentTime - boostStarTime;
	}

}
