using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public GameObject[] decoration;
    public Transform[] spawnPoints;
    //public float spawnTime = 3f;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Spawn(i);
        }
	}
	
	// Update is called once per frame
	void Spawn (int spawnPointIndex)
    {
        int decorationList = Random.Range(0, decoration.Length);
        Instantiate(decoration[decorationList], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
