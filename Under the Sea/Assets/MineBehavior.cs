 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour {

	public GameObject Player;
	private bool entered;
	public float speed;
	
	public float step = 0.01f;
	Vector3 del;
	float dist;

	Vector3 lookAt;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			entered = true;
		}
		else{
			entered = false;
		}

	}
	// Update is called once per frame
	void FixedUpdate () {
		if(entered)
		{
			step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
		}
	}

}
