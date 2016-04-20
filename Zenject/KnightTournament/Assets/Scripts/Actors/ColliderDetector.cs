using UnityEngine;
using System.Collections;

public class ColliderDetector : MonoBehaviour {

    public delegate void TriggerEventDelegate(GameObject sender, Collider2D collider);
    public event TriggerEventDelegate TriggerEnter;
    public event TriggerEventDelegate TriggerExit;

   

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (TriggerEnter!= null)
            TriggerEnter(gameObject,collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (TriggerExit != null)
            TriggerExit(gameObject, collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (TriggerEnter != null)
            TriggerEnter(gameObject, collision.collider);
     
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (TriggerExit != null)
            TriggerExit(gameObject, collision.collider);
    }
}

