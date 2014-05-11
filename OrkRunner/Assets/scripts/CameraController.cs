using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	private Transform playerPosition;

	void Start () {
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	private void FixedUpdate(){
		transform.position = new Vector3(playerPosition.position.x,playerPosition.position.y,transform.position.z);



	}

}
