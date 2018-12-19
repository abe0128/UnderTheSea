using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Matthew Connolly
// This is the script for the crap
public class enemyType1 : MonoBehaviour {
	// Use this for initialization
     public enum OccilationFuntion { Sine, Cosine }
	 public GameObject player;


     public int health;
     public void Start ()
     {
         health = 3;
         //to start at zero
         StartCoroutine (Oscillate (OccilationFuntion.Sine, 0.05f)); // starts the movement routine
         //to start at scalar value
         //StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));
     }
    
 	 void OnCollisionEnter(Collision other)
	 {
		if(other.gameObject.tag == "Rock")
		{
			health -= 1; // decrease health if collides with rock
		}
        if(other.gameObject.tag == "KelpWhip")
        {
            StopAllCoroutines(); // this doesn't work but was testing out to see if kelp whip could stop the movement of the enemy
        }
	 }

    //  void OnCollisionExit(Collision other)
    //  {
    //      if(other.gameObject.tag == "KelpWhip")
    //      {
    //          StartCoroutine(Oscillate(OccilationFuntion.Sine, 0.05f));
    //      }
    //  }
     private IEnumerator Oscillate (OccilationFuntion method, float scalar) // movement script for the eneemy to move back and forth on the z axis
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
            DestroyObject(this.gameObject); // destroy the object when health is 0
        }
     }
}
