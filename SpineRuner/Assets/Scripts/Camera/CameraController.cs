using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {


    private Transform playerTransform;
    private Camera camera;
    void Start()
    {
        playerTransform = GameObject.Find("ork").transform;
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {

       // playerDeltaX = playerPosition.position.x - playerStartPosition.x;
       // playerDeltaY = playerPosition.position.y - playerStartPosition.y;
       // curPositionX = followX ? playerDeltaX + startX : transform.position.x;
       // curPositionY = followY ? playerDeltaY + startY : transform.position.y;
       // transform.position = new Vector3(curPositionX, curPositionY, transform.position.z);

        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y,transform.position.z);
        HandleArrows();
    }

    private void HandleArrows()
    {


        if (Input.GetKey("up"))
        {
            camera.orthographicSize++;
            Debug.Log(camera.orthographicSize);
        }
        else if (Input.GetKey("down"))
        {
            camera.orthographicSize--;
            Debug.Log(camera.orthographicSize);
        }

    }


    
}
