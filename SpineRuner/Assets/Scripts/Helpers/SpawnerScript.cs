using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {


	public GameObject spawnPlace;
	public GameObject[] grounds;

	void Start () {
		spawnPlace = GameObject.FindGameObjectWithTag("SpawnPlace");
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag != TagEnum.Spawn)
			return;
		GameObject ground = grounds[Random.Range(0,grounds.GetLength(0))];
        Destroy(collider.gameObject);
		Instantiate(ground,spawnPlace.transform.position, new Quaternion());
	}
}
