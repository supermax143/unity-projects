using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
        SkeletonAnimation animation = GetComponent<SkeletonAnimation>();
        animation.state.SetAnimation(1, "throw",true);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
