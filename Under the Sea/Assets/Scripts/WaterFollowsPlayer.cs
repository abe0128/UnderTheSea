using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollowsPlayer : MonoBehaviour {

    private GameObject player;  // Holds players object

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        // Follow player on the x and z axis
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
