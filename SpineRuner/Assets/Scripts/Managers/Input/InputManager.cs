﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {




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