using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float speed;
	Vector3 initialPosition;
	int direction;
	public float maxDist;
	public float minDist;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		direction = -1;
		maxDist += transform.position.x;
		minDist -= transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		switch(direction)
		{
			case -1:
			//Enemy Move Left
				if(transform.position.x > minDist)
				{
					GetComponent<Rigidbody>().velocity = new Vector3(-speed, GetComponentInParent<Rigidbody>().velocity.y, transform.position.z);
				}
				else{
					direction = 1;
				}
				break;
			case 1:
			// Enemy Move Right
				if(transform.position.x < maxDist)
				{
					GetComponent<Rigidbody>().velocity =  new Vector3(speed, GetComponentInParent<Rigidbody>().velocity.y, transform.position.z);
				}
				else{
					direction = -1;
				}
				break;
		}
	}
}
