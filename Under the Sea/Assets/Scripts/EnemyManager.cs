using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	public GameObject[] enemyType1;
	public GameObject[] enemyType2;
	public GameObject[] enemyType3;
	public GameObject[] enemyType4;

	public Transform[] SpawnEnemyT1;
	public GameObject player;

	private float distance;

	// Use this for initialization
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update(){

	}

	private void FixedUpdate()
	{
		foreach(Transform spawnLocation in SpawnEnemyT1)
		{
			distance = Vector3.Distance(spawnLocation.transform.position, player.transform.position);
			if(distance < 5.0f)
			{
				SpawnEnemy1(spawnLocation);
			}
		}
	}

	void SpawnEnemy1(Transform spawnPoint)
	{
		int enemyType1List = Random.Range(0, enemyType1.Length);
		Instantiate(enemyType1[enemyType1List], spawnPoint);
	}
}
