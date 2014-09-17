using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.gameObject.tag == TagEnum.Player)
        {
        }
        else if (collider.gameObject.tag == TagEnum.Enemy)
            Destroy(collider.transform.parent.gameObject);
        else if (collider.gameObject.tag == TagEnum.RightBound)
            DestroyGround(collider);
        else
            HandleDefaulDestroy(collider);
	}

    private void DestroyGround(Collider2D collider)
    {
        Destroy(collider.transform.parent.gameObject);
    }



    private void HandleDefaulDestroy(Collider2D collider)
    {
        if (collider.gameObject.tag == TagEnum.Ground)
            return;

        if (!collider.transform.parent) { 
            Destroy(collider.gameObject);
            return;
        }
        Destroy(collider.transform.parent.gameObject);
    }

    

    


}
