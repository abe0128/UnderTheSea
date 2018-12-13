using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	public GameObject[] enemyType1;
	public GameObject enemyType2;
	public GameObject enemyType3;
	public GameObject[] enemyType4;

	public List<GameObject> SpawnEnemyT1;
	public List<GameObject> SpawnEnemyT2;
	public List<GameObject> SpawnEnemyT3;
	public GameObject player;

	private float distance;

	// private bool EType1Spawning = false;

	// Use this for initialization
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update(){

	}

	private void FixedUpdate()
	{
		enemyTypeChecker();
	}

	void enemyTypeChecker(){
		foreach(GameObject spawnLocation in SpawnEnemyT1)
		{
			if(spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn)
			{
				SpawnEnemy1(spawnLocation);
				spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn = false;
				spawnLocation.gameObject.GetComponent<SphereCollider>().enabled = false;
			}
		}
		foreach(GameObject spawnLocation in SpawnEnemyT2)
		{
			if(spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn)
			{
				SpawnEnemy2(spawnLocation);
				spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn = false;
				spawnLocation.gameObject.GetComponent<SphereCollider>().enabled = false;
			}
		}
		foreach(GameObject spawnLocation in SpawnEnemyT3)
		{
			if(spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn)
			{
				SpawnEnemy3(spawnLocation);
				spawnLocation.gameObject.GetComponent<SpawnLocationBehaviour>().canSpawn = false;
				spawnLocation.gameObject.GetComponent<SphereCollider>().enabled = false;
			}
		}
	}

	void SpawnEnemy1(GameObject spawnPoint)
	{
		int enemyType1List = Random.Range(0, enemyType1.Length);
		Instantiate(enemyType1[enemyType1List], spawnPoint.transform);
	}
	void SpawnEnemy2(GameObject spawnPoint)
	{
		Instantiate(enemyType2,spawnPoint.transform);
	}
	void SpawnEnemy3(GameObject spawnPoint)
	{
		Instantiate(enemyType3,spawnPoint.transform);
	}
}
