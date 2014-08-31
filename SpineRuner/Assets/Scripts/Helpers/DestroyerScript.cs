using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D collider)
	{
        switch (collider.gameObject.tag)
        {
            case "Player":
                break;
            case "Enemy":
                Destroy(collider.transform.parent.gameObject);
                break;
            default:
                HandleDefaulDestroy(collider);
                break;
        }
	}

    private void HandleDefaulDestroy(Collider2D collider)
    {
        if (!collider.transform.parent) { 
            Destroy(collider.gameObject);
            return;
        }
        if (collider.transform.parent.transform.childCount==1)
        {
            Destroy(collider.transform.parent.gameObject);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }

    

    


}
