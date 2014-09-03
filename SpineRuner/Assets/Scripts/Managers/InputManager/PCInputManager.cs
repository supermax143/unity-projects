using UnityEngine;
using System.Collections;

public class PCInputManager : InputManager
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        isJumpPressed = Input.GetButton("Jump");
        
        if(Input.GetMouseButtonDown(0))
            isFirePressed = true;
		else if(Input.GetMouseButtonUp(0))
            isFirePressed = false;
	}
}
