using UnityEngine;
using System.Collections;

public class MobileInputManager : InputManager
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool isFirePressedBuffer=false;
        bool isJumpPressedBuffer=false;
	    foreach(Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
                isJumpPressedBuffer = true;
            if (touch.position.x > Screen.width / 2)
                isFirePressedBuffer = true;
        }
        isFirePressed = isFirePressedBuffer;
        isJumpPressed = isJumpPressedBuffer;
	}
}
