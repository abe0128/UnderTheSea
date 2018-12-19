using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Matthew Connolly
// This is the script for the squid enemy
public class enemyType3 : MonoBehaviour {

	public enum OccilationFunction{Sine, Cosine} /

	public GameObject player;

	public int health;
	// Use this for initialization
	void Start () {
		health = 3;

		StartCoroutine(Oscillate(OccilationFunction.Sine, 0.05f)); // starts the movement of the squid going up and down
	}

	 void OnCollisionEnter(Collision other)
	 {
		// decrease health if enemy collides with rock
		if(other.gameObject.tag == "Rock")
		{ 
			health -= 1;
		}
        // if(other.gameObject.tag == "KelpWhip")
        // {
        //     StopAllCoroutines();
        // }
	 } 

    //  void OnCollisionExit(Collision other)
    //  {
    //      if(other.gameObject.tag == "KelpWhip")
    //      {
    //          StartCoroutine(Oscillate(OccilationFunction.Sine, 0.05f));
    //      }
    //  }
	
// move enemy up and down the y axis and not x or z axis
	private IEnumerator Oscillate (OccilationFunction method, float scalar)
	{
		while(true)
		{
			if(method == OccilationFunction.Sine)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y+Mathf.Sin(Time.time)*scalar, transform.position.z);
			}
			else if (method == OccilationFunction.Cosine)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Cos(Time.time) * scalar, transform.position.z );
            }
            yield return new WaitForEndOfFrame ();
		}
	}

	// Update is called once per frame
	void Update () {
		if(health == 0)
		{
			DestroyObject(this.gameObject); // desroy object if health is 0
		}
	}
}
