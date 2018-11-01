using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float swimVelocity;                  // Changeable value, swim velocity

    private PlayerCamFollow playerCamFollow;  // To hold the script of the 
    private float stamina;                      // To hold the player's stamina
    private float distanceToGround;             // To hold the current disntance from ground
    private float stamina_offset;               // To hold the countdown to stamina offset
    private float boost_offset;                 // To hold the boost offset, for boost effect
    private bool facing_right;                  // To hold a temporary value till we find a more effective way to get which direction player is facing
    private bool facing_left;                   // To hold a temporary value till we find a more effective way to get which direction player is facing 


    // Use this for initialization
    void Start() {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>(); ;
        distanceToGround = GetComponent<Collider>().bounds.extents.y;   // set value to to the bottom of the object
        stamina = 100.0f;                                               // set base stamina to 100 (int)
        stamina_offset = 0.0f;                                          // set stamina offset to 0.0f (float), later to reset stamina off-time
        boost_offset = 0.0f;                                            // set boost_offset to 0.0f (float)
        facing_right = true;                                            // set facing_right direction temp, true
        facing_left = false;                                            // set facing_left direction temp, false
    }

    // Update is called once per frame
    private void Update() {
        StaminaRegen();                             // Always check to see if we can regenerate players stamina
        PlayerMovement();                           // Calls PlayerMoment function, allows drift motion (left to right)
        PlayerSwim();                               // Calls PlayerSwim function, allows swimming motion (up)
        GetComponent<Rigidbody>().drag = 4;         // Set player's drag to four, from Ridigbody component (illusion of underwater)
    }

    /// <summary>
    /// IsGrounded Method, Boolean return
    /// True if the objects bottom collider is on the ground 0.1f off the ground that is
    /// False, if the object is off of the ground
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    /// <summary>
    /// PlayerMovement method, basic movement of player, left and right
    /// </summary>
    private void PlayerMovement()
    {
        // move player right
        if (Input.GetKey(KeyCode.D))
        {
            facing_right = true;
            facing_left = false;
            transform.position = new Vector3(transform.position.x + (playerCamFollow.GetX()), transform.position.y, transform.position.z + (playerCamFollow.GetZ()));
        }
        // move player left
        if (Input.GetKey(KeyCode.A))
        {
            facing_right = false;
            facing_left = true;
            transform.position = new Vector3(transform.position.x + (-playerCamFollow.GetX()), transform.position.y, transform.position.z + (-playerCamFollow.GetZ()));
        }
    }

    /// <summary>
    /// PlayerSwim method, when space bar is pressed, player swims (repeated option)
    /// </summary>
    private void PlayerSwim()
    {
        // Start boost_offset as long as you're holding the space bar
        if (Input.GetKey(KeyCode.Space) && stamina > 0)
        {
            boost_offset++;
        }
        // Player Jumps on space bar, as long as jumped == false, and boost_offset is less than a second
        if (Input.GetKeyUp(KeyCode.Space) && boost_offset < 60 && stamina > 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * swimVelocity; // Player Swim, uses Rigidbody, set for player's vector to swim up * swimvelocity
            stamina -= 3;                                                   // Reduce stamina upon swim physics
            boost_offset = 0.0f;                                            // Reset boost_offset back to 0.0
        }
        // Player boost on held space bar, as long as space bar is held longer than 1 second
        else if (Input.GetKeyUp(KeyCode.Space) && boost_offset >= 60 && stamina > 0)
        {
            // Temporary bool statement, if facing right boost shoot right
            if (facing_right)
            {
                GetComponent<Rigidbody>().velocity = Vector3.right * swimVelocity * 5f; // Player Swim, uses Rigidbody, set for player's vector to swim up * swimvelocity
                stamina -= 10;                                                          // Reduce stamina upon swim physics
                boost_offset = 0.0f;                                                    // Reset boost_offset back to 0.0
            }
            // Temporary bool statement, if facing left boost shoot left
            if (facing_left)
            {
                GetComponent<Rigidbody>().velocity = Vector3.left * swimVelocity * 5f;  // Player Swim, uses Rigidbody, set for player's vector to swim up * swimvelocity
                stamina -= 10;                                                          // Reduce stamina upon swim physics
                boost_offset = 0.0f;                                                    // Reset boost_offset back to 0.0
            }
        }
    }

    /// <summary>
    /// StaminaRegen method, regenerates stamina bar after three seconds
    /// </summary>
    private void StaminaRegen()
    {
        // When IsGrounded is true, start timer, else, set stamina_offset to 0.0f
        if (IsGrounded())
        {
            stamina_offset++;
        }
        else
        {
            stamina_offset = 0.0f;
        }
        
        // Regenerate stamina when rested after, 3 seconds
        if (stamina_offset >= 180)
        {
            stamina += 0.05f;
        }
        //Debug.Log(stamina);

        stamina = (stamina < 0) ? 0 : stamina;      // ternary operator, Stamina can't go under 0
        stamina = (stamina > 100) ? 100 : stamina;  // ternary operator, Stamina can't exceed 100
    }
}
