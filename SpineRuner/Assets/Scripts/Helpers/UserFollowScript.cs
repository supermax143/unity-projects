using UnityEngine;
using System.Collections;

public class UserFollowScript : MonoBehaviour {


	private float startX;
	private float startY;
	private float curPositionX;
	private float curPositionY;
	private Transform playerTransform;
	private Vector3 playerStartPosition; 
	private float playerDeltaX;
	private float playerDeltaY;

	public bool followX = true;
	public bool followY = true;


	void Start () {
		playerTransform = GameObject.Find("player").transform;
		playerStartPosition = new Vector3(playerTransform.position.x,playerTransform.position.y);
		startX = transform.position.x;
		startY = transform.position.y;
	}
	
	private void FixedUpdate(){
		playerDeltaX = playerTransform.position.x - playerStartPosition.x;
		playerDeltaY = playerTransform.position.y - playerStartPosition.y;
		curPositionX = followX?playerDeltaX+startX:transform.position.x;
		curPositionY = followY?playerDeltaY+startY:transform.position.y;
		transform.position = new Vector3(curPositionX,curPositionY,transform.position.z);
		
	}

}
