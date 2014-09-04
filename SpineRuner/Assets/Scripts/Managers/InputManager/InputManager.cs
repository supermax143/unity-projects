using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    protected const float MIN_ANGLE = 30f;
    protected const float MAX_ANGLE = 80f;

    private static InputManager instance;

    public static InputManager Instance
    {
        get { return InputManager.instance; }
    }

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

	void Start () {
	    switch(Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
                instance = GetComponentInChildren<PCInputManager>();
                break;
            default :
                instance = GetComponentInChildren<MobileInputManager>();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
