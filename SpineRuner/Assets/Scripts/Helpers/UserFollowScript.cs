using UnityEngine;
using System.Collections;

public class UserFollowScript : MonoBehaviour {


	private float startX;
	private float startY;
	private float curPositionX;
	private float curPositionY;
	private Transform playerPosition;
	private Vector3 playerStartPosition; 
	private float playerDeltaX;
	private float playerDeltaY;

	public bool followX = true;
	public bool followY = true;


	void Start () {
		playerPosition = GameObject.Find("player").transform;
		playerStartPosition = new Vector3(playerPosition.position.x,playerPosition.position.y);
		startX = transform.position.x;
		startY = transform.position.y;
	}
	
	private void FixedUpdate(){
		playerDeltaX = playerPosition.position.x - playerStartPosition.x;
		playerDeltaY = playerPosition.position.y - playerStartPosition.y;
		curPositionX = followX?playerDeltaX+startX:transform.position.x;
		curPositionY = followY?playerDeltaY+startY:transform.position.y;
		transform.position = new Vector3(curPositionX,curPositionY,transform.position.z);
		
	}

}
