using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;         // Changeable value, players speed
    public float jumpVelocity;  // Changeable value, jump velocity1
    private bool jumped;        // Bool value, assigned when player jumps

    // Use this for initialization
    void Start() {
        jumped = false;     // Initialize jumped to false
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement();       // Player moves from left to right
        PlayerJump();           // Player jumps
    }

    /// <summary>
    /// OnCollisionEnter method, Set collisions that player enters
    /// Ground: Used to set jumped to false, when player hits the ground
    /// Ladder: Set climbable to true
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // If player lands back on ground, set jumped to false, so player can jump again
        if (collision.gameObject.tag == "Ground")
        {
            jumped = false;
        }
    }

    /// <summary>
    /// PlayerMovement method, basic movement of player, left and right
    /// </summary>
    private void PlayerMovement()
    {
        // move player right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            //GetComponent<Rigidbody>().velocity = Vector3.left * speed;
        }
        // move player left
        if (Input.GetKey(KeyCode.A))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.right * speed;
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// PlayerJump method, when space bar is pressed, player jumps
    /// </summary>
    private void PlayerJump()
    {
        // Player Jumps on space bar, as long as jumped == false
        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            jumped = !jumped;
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
        }

    }
}
