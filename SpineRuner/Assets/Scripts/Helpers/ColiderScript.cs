using UnityEngine;
using System.Collections;

public class ColiderScript : MonoBehaviour {

    public string colliderName;

    public delegate void CollisionEvent(string sender, Collider2D collider);
    public CollisionEvent collisionEvent;
    
	


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collisionEvent!= null)
            collisionEvent(colliderName,collider);
    }

}
