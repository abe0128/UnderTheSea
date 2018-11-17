using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType2 : MonoBehaviour {

	// Use this for initialization
public enum OccilationFuntion { Sine, Cosine }
	 public GameObject player;

	 public int health;

	 float enemyPosY;
     public void Start ()
     {
         //to start at zero
         StartCoroutine (Oscillate (OccilationFuntion.Sine, 0.05f));
         //to start at scalar value
         //StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));
		 player = GameObject.FindGameObjectWithTag("Player");
		 health = 1;
		 transform.position = new Vector3( transform.position.x, 6, transform.position.z);
     }

	 void OnCollisionEnter(Collision other)
	 {
		if(other.gameObject.tag == "Player")
		{
			health -= 1;
		}

	 }
 
     private IEnumerator Oscillate (OccilationFuntion method, float scalar)
     {
         while (true)
         {
			 if(player.transform.position.y > 2)
			 {
				 enemyPosY = (player.transform.position.y - 1.5f);
			 }
			 else
			 {
				 enemyPosY = transform.position.y;
			 }
             if (method == OccilationFuntion.Sine)
             {
    			transform.position = new Vector3 (transform.position.x, enemyPosY, transform.position.z + Mathf.Sin (Time.time) * scalar);
             }
             else if (method == OccilationFuntion.Cosine)
             {
                 transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z + Mathf.Cos(Time.time) * scalar);
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
