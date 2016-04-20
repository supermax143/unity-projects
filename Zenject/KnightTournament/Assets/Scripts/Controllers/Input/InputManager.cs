using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private static InputManager instance;

    public static InputManager Instance
    {
        get { return InputManager.instance; }
    }

    protected bool isGoLeft;
    protected bool isGoRight;
    protected bool spearUp = false;
    protected bool spearDown = false;


    /// <summary>
    /// getters
    /// </summary>
    public bool SpearDown
    {
        get { return spearDown; }
    }
    public bool SpearUp
    {
        get { return spearUp; }
    }
    public bool IsGoLeft
    {
        get { return isGoLeft; }
    }

    public bool IsGoRight
    {
        get { return isGoRight; }
    }

    /// <summary>
    /// 
    /// </summary>

	void Start () {
	    switch(Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                instance = gameObject.AddComponent<MobileInputManager>();
                break;
            default :
                instance = gameObject.AddComponent<PCInputManager>();
                break;
        }
	}
	
    protected void init()
    {
       
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
