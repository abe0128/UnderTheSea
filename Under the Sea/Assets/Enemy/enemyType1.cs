using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType1 : MonoBehaviour {
	// Use this for initialization
     public enum OccilationFuntion { Sine, Cosine }
	 public GameObject player;


     public int health;
     public void Start ()
     {
         health = 1;
         //to start at zero
         StartCoroutine (Oscillate (OccilationFuntion.Sine, 0.05f));
         //to start at scalar value
         //StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));
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
    //          StartCoroutine(Oscillate(OccilationFuntion.Sine, 0.05f));
    //      }
    //  }
     private IEnumerator Oscillate (OccilationFuntion method, float scalar)
     {
         while (true)
         {
             if (method == OccilationFuntion.Sine)
             {
                 transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Mathf.Sin (Time.time) * scalar);
             }
             else if (method == OccilationFuntion.Cosine)
             {
                 transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Mathf.Cos(Time.time) * scalar);
             }
             yield return new WaitForEndOfFrame ();
         }
     }

     public void Update()
     {
        if(health == 0)
        {
            DestroyObject(this.gameObject);
        }
     }
}
