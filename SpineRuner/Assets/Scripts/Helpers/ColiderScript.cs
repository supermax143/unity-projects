using UnityEngine;
using System.Collections;

public class ColiderScript : MonoBehaviour {

    public string colliderName;

    public delegate void CollisionEvent(string sender, Collider2D collider);
    public CollisionEvent collisionEnterEvent;
    public CollisionEvent collisionExitEvent;
	


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collisionEnterEvent!= null)
            collisionEnterEvent(colliderName,collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collisionExitEvent != null)
            collisionExitEvent(colliderName, collider);
    }
}
