using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	// Use this for initialization

    public GameObject [] booms;

	void Start () {
        ColiderScript cs = GetComponent<ColiderScript>();
        cs.TriggerEnter += OnTriggerEnter;
        cs.CollisionEnter += OnCollisionEnter;
    }

    void OnCollisionEnter(string sender, Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    void OnTriggerEnter(string sender, Collider2D collider)
    {
        HandleCollision(collider.gameObject);
    }

    private void HandleCollision(GameObject targetGameObject)
    {
        if(targetGameObject.tag == TagEnum.Player)
            return;
        gameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        GameObject boomPref = booms[Random.Range(0, booms.GetLength(0))];
        GameObject boom = (GameObject)Instantiate(boomPref, transform.position, Quaternion.AngleAxis(Random.Range(-50, 50), Vector3.forward));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
