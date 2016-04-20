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
        bool goRight=false;
        bool goLeft=false;
        bool sUp = false;
        bool sDown = false;
	    foreach(Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
                goRight = true;
            else if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
                goLeft = true;

            if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
                sDown = true;
            else if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
                sUp = true;
        }
        isGoRight = goRight;
        isGoLeft = goLeft;
        spearUp = sUp;
        spearDown = sDown;
	}




}
