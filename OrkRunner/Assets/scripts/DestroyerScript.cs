using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
			return;
		Destroy(collider.gameObject);
	}
}
