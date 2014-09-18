using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    public float spawnDistance = 10;
    
    private Transform playerTransform;
    private Transform rightBoundTransforn;

	public GameObject[] grounds;
    public GameObject[] enemies;
    private int numGroundsAddad = 0;

	void Start () {
        playerTransform = GameObject.Find("player").transform;
        UpdateRightBound();
        UpdateSpawnDistance();
	}

    private void UpdateSpawnDistance()
    {
        Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        spawnDistance = Vector2.Distance(p1,p2);
    }

    private void UpdateRightBound()
    {
        foreach(GameObject bound in GameObject.FindGameObjectsWithTag(TagEnum.RightBound))
        {
            if (!rightBoundTransforn || rightBoundTransforn.position.x < bound.transform.position.x)
                rightBoundTransforn = bound.transform;
        }
    }

    private void Update()
    {
        if (!playerTransform || !rightBoundTransforn)
            return;
        Debug.DrawLine(playerTransform.position,rightBoundTransforn.position);
        Vector2.Distance(playerTransform.position, rightBoundTransforn.position);
        if(Vector2.Distance(playerTransform.position, rightBoundTransforn.position)<spawnDistance)
        {
            GameObject ground = AddGround();
            if(numGroundsAddad % 2 == 0)
                AddEnemy(ground);
            UpdateRightBound();
        }
    }

    private void AddEnemy(GameObject ground)
    {
        GameObject enemyPref = enemies[Random.Range(0, enemies.GetLength(0))];
        GameObject enemy = (GameObject)Instantiate(enemyPref, ground.transform.position, new Quaternion());
         
    }

    private GameObject AddGround()
    {
        numGroundsAddad++;
        GameObject groundPref = grounds[Random.Range(0, grounds.GetLength(0))];
        return (GameObject)Instantiate(groundPref, rightBoundTransforn.position, new Quaternion());
    }



}
