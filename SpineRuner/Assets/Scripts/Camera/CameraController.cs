using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {


    private Transform playerTransform;
    private Camera cameraComponent;
    void Start()
    {
        playerTransform = GameObject.Find("player").transform;
        cameraComponent = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {

        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y,transform.position.z);
        HandleArrows();
    }

    private void HandleArrows()
    {


        if (Input.GetKey("up"))
        {
            cameraComponent.orthographicSize++;
            Debug.Log(cameraComponent.orthographicSize);
        }
        else if (Input.GetKey("down"))
        {
            cameraComponent.orthographicSize--;
            Debug.Log(cameraComponent.orthographicSize);
        }

    }


    
}
