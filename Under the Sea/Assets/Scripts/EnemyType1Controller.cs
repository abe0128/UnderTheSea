using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Controller : MonoBehaviour {

	// Use this for initialization
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
				if(transform.position.z > minDist)
				{
					//GetComponent<Rigidbody>().velocity = new Vector3(transform.position.x, GetComponent<Rigidbody>().velocity.y, -speed);
					//GetComponent<Rigidbody>().velocity = new Vector3(-speed, transform.position.y, GetComponent<Rigidbody>().velocity.z);
					GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -speed, transform.position.z);
				}
				else{
					direction = 1;
				}
				break;
			case 1:
			// Enemy Move Right
				if(transform.position.z < maxDist)
				{
					GetComponent<Rigidbody>().velocity =  new Vector3(transform.position.x, GetComponent<Rigidbody>().velocity.y, speed);
				}
				else{
					direction = -1;
				}
				break;
		}
	}
}
