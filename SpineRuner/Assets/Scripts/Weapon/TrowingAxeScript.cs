using UnityEngine;
using System.Collections;

public class TrowingAxeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
         if (collider.tag != TagEnum.Enemy)
            return;
        rigidbody2D.velocity = new Vector2(-0.5f, 0);
        rigidbody2D.AddTorque(100);
        Destroy(GetComponent<Collider2D>());
            
    }
}
