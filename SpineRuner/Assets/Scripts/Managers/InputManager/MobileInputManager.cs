using UnityEngine;
using System.Collections;

public class MobileInputManager : InputManager
{

	// Use this for initialization
	void Start () 
    {
        init();
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
            {
                isFirePressedBuffer = true;
                HandleFirePressed(touch);
            }
        }
        isFirePressed = isFirePressedBuffer;
        isJumpPressed = isJumpPressedBuffer;
	}

    private void HandleFirePressed(Touch touch)
    {
        Vector2 p1 = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 p2 = FirePosition;
        fireAngle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * (180 / Mathf.PI) + 180;
    }



}
