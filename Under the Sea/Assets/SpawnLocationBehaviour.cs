using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationBehaviour : MonoBehaviour {

	// Use this for initialization
	public bool canSpawn = false;
	void Start () {
		
	}
	
	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player"){
			canSpawn = true;
		}
		else{
			canSpawn = false;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
