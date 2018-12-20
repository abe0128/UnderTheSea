using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Matthe Connolly
// This script is for the shark enemy
public class enemyType2 : MonoBehaviour {

    // Use this for initialization
    public enum OccilationFuntion { Sine, Cosine } // create Enum for occilation.
    public GameObject player;

    public int health;

    float enemyPosY;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // grab player object
                                                             //to start at zero
        StartCoroutine(Oscillate(OccilationFuntion.Sine, 0.1f)); // start movment of the shark enemy on the z axis
                                                                 //to start at scalar value
                                                                 //StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));

        health = 4;
        transform.position = new Vector3(transform.position.x, 6, transform.position.z); // give him a starting positoin
    }

    //  void OnCollisionEnter(Collision other)
    //  {
    // 	if(other.gameObject.tag == "Player")
    // 	{
    // 		health -= 1;
    // 	}

    //  }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rock")
        {
            health--; // if shark collides with player's rock his health decreases
        }
    }

    //this starts the movement of the enemy
    private IEnumerator Oscillate(OccilationFuntion method, float scalar)
    {
        while (true)
        {
            if (player.transform.position.y > 2)
            {
                enemyPosY = (player.transform.position.y - 1.5f);
            }
            else
            {
                enemyPosY = transform.position.y;
            }
            if (method == OccilationFuntion.Sine)
            {
                transform.position = new Vector3(transform.position.x, enemyPosY, transform.position.z + Mathf.Sin(Time.time) * scalar); //moves enemy on the z axis while following the player on the y axis

            }
            else if (method == OccilationFuntion.Cosine)
            {
                transform.position = new Vector3(transform.position.x, enemyPosY, transform.position.z + Mathf.Cos(Time.time) * scalar);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Update()
    {//destroy enemy if health is 0
        if (health == 0)
        {
            DestroyObject(this.gameObject);
        }
    }
}
