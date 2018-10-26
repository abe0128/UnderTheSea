using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectThem : MonoBehaviour {
	public static int foods = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider gamer)
	{
		if (gamer.gameObject.tag == "Player") {
			foods++;
		}
	}
}
