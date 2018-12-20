 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Matthew Connolly
// This is the behaviour of the mine.
public class MineBehavior : MonoBehaviour {

    private GameObject Player;
    private bool entered;
    public float speed;

    public float step = 0.01f;
    Vector3 del;
    float dist;

    Vector3 lookAt;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // finds player object
    }

    private void OnTriggerEnter(Collider col) // if player is close it starts to follow
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("IN");
            entered = true;
        }

    }
    private void OnTriggerExit(Collider col) //stops following
    {
        if (col.gameObject.tag == "Player")
        {
            entered = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (entered) // starts following the player if in range
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        }
    }

}
