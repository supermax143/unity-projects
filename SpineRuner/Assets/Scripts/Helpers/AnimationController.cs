using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    SkeletonAnimation skeletonAnimation;
    public bool flipX = false;
	void Start () {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.skeleton.FlipX = flipX;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
