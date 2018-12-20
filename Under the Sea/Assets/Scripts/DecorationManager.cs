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
        for (int i = 0; i < spawnPoints.Length; i++) //array to place spawn point objects for objects to be spawned in
        {
            Spawn(i);
        }
	}
	
	void Spawn (int spawnPointIndex)
    {
        int decorationList = Random.Range(0, decoration.Length); //place decorations randomly instead of placing the same one every time game is initialize
        Instantiate(decoration[decorationList], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // place object(decorations) on these spawnpoints
	}
}
