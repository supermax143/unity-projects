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
        isGoLeft = Input.GetKey(KeyCode.LeftArrow);
        isGoRight = Input.GetKey(KeyCode.RightArrow);
        spearUp = Input.GetKey(KeyCode.UpArrow);
        spearDown = Input.GetKey(KeyCode.DownArrow);
	}

}
