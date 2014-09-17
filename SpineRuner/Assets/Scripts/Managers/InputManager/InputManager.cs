using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private static InputManager instance;

    public static InputManager Instance
    {
        get { return InputManager.instance; }
    }


    protected const float MIN_ANGLE = 30f;
    protected const float MAX_ANGLE = 80f;
    
    private GameObject player;
    private Spine.Bone fireBone;



    protected bool isJumpPressed;
    public bool IsJumpPressed
    {
        get { return isJumpPressed; }
    }

    protected bool isFirePressed;
    public bool IsFirePressed
    {
        get { return isFirePressed; }
    }

    protected float fireAngle = MIN_ANGLE;

    public float FireAngle
    {
        get { return fireAngle; }
    }

    public Vector2 FirePosition
    {
        get 
        {
            return new Vector3(player.transform.position.x + fireBone.worldX, player.transform.position.y + fireBone.worldY);
        }
    }

	void Start () {
	    switch(Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                instance = gameObject.AddComponent<MobileInputManager>();//GetComponentInChildren<MobileInputManager>();
                break;
            default :
                instance = gameObject.AddComponent<PCInputManager>();//GetComponentInChildren<PCInputManager>();
                break;
        }
	}
	
    protected void init()
    {
        player = GameObject.Find("player");
        fireBone = player.GetComponent<SkeletonAnimation>().skeleton.FindBone("axe");
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
