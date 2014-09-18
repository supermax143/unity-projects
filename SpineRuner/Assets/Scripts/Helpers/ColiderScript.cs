using UnityEngine;
using System.Collections;

public class ColiderScript : MonoBehaviour {

    public string colliderName;

    public delegate void TriggerEventDelegate(string sender, Collider2D collider);
    public event TriggerEventDelegate TriggerEnter;
    public event TriggerEventDelegate TriggerExit;

    public delegate void CollisionEventDelegate(string sender, Collision2D collision);
    public event CollisionEventDelegate CollisionEnter;
    public event CollisionEventDelegate CollisionExit;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (TriggerEnter!= null)
            TriggerEnter(colliderName,collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (TriggerExit != null)
            TriggerExit(colliderName, collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionEnter != null)
            CollisionEnter(colliderName, collision);
     
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (CollisionExit != null)
            CollisionExit(colliderName, collision);
    }
}

