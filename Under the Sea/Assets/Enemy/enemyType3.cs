using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType3 : MonoBehaviour {

	public enum OccilationFunction{Sine, Cosine}

	public GameObject player;

	public int health;
	// Use this for initialization
	void Start () {
		health = 1;

		StartCoroutine(Oscillate(OccilationFunction.Sine, 0.05f));
	}

	 void OnCollisionEnter(Collision other)
	 {
		if(other.gameObject.tag == "rock")
		{
			health -= 1;
		}
        if(other.gameObject.tag == "KelpWhip")
        {
            StopAllCoroutines();
        }
	 }

    //  void OnCollisionExit(Collision other)
    //  {
    //      if(other.gameObject.tag == "KelpWhip")
    //      {
    //          StartCoroutine(Oscillate(OccilationFunction.Sine, 0.05f));
    //      }
    //  }
	

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
			DestroyObject(this.gameObject);
		}
	}
}
