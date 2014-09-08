using UnityEngine;
using System.Collections;

public class PCInputManager : InputManager
{

	// Use this for initialization
	void Start () {
        init();
	}
	
    

	// Update is called once per frame
	void Update () {
        isJumpPressed = Input.GetButton("Jump");
        
        if(Input.GetMouseButtonDown(0))
        {
            isFirePressed = true;

        }
		else if(Input.GetMouseButtonUp(0))
            isFirePressed = false;

        if (Input.mousePosition!=null)
        {
            Vector2 p1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 p2 = FirePosition;
            fireAngle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) *(180 / Mathf.PI) + 180;
            //Debug.DrawLine(FirePosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //Debug.Log(fireAngle);
          
        }
	}
}
