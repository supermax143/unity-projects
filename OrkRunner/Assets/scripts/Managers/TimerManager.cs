using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {



	private static TimerManager instance;

	private float currentTime = 0f;

	void Start () { 
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		currentTime += 1.0f*Time.deltaTime;
	}

	public float CurrentTime {
		get {
			return currentTime;
		}
	}

	public static TimerManager Instance {
		get {
			return instance;
		}
	}
}
