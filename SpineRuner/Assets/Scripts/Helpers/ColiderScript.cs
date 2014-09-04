using UnityEngine;
using System.Collections;

public class ColiderScript : MonoBehaviour {

    public string colliderName;

    public delegate void TriggerEvent(string sender, Collider2D collider);
    public TriggerEvent triggerEnterEvent;
    public TriggerEvent triggerExitEvent;

    public delegate void CollisionEvent(string sender, Collision2D collision);
    public CollisionEvent collisionEnterEvent;
    public CollisionEvent collisionExitEvent;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (triggerEnterEvent!= null)
            triggerEnterEvent(colliderName,collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (triggerExitEvent != null)
            triggerExitEvent(colliderName, collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionEnterEvent != null)
            collisionEnterEvent(colliderName, collision);
     
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collisionExitEvent != null)
            collisionExitEvent(colliderName, collision);
    }
}

