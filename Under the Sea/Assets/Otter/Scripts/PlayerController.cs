using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float swimVelocity;                  // Changeable value, swim velocity

    private PlayerAnimation current_animation;  // To hold the current otter animation
    private PlayerCamFollow playerCamFollow;    // To hold the script of the 
    private Animator anim;                      // To hold the otter animator, (which animation to play)
    private float stamina;                      // To hold the player's stamina
    private float distanceToGround;             // To hold the current disntance from ground
    private float stamina_offset;               // To hold the countdown to stamina offset

    /// <summary>
    /// PlayerAnimation: Enum operator, For player animation
    /// 1. Idle: Animation for no movement
    /// 2. Swim: Animation for swimming (left and right movement)
    /// 3. Attack: Animation for rock throw attack
    /// </summary>
    private enum PlayerAnimation
    {
        Idle,
        Swim,
        Attack
    }

    // Use this for initialization
    void Start()
    {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>();
        current_animation = PlayerAnimation.Idle;                       // set current animation to Idle
        anim = GetComponent<Animator>();                                // set Animator component to anim
        distanceToGround = GetComponent<Collider>().bounds.extents.y;   // set value to to the bottom of the object
        stamina = 100.0f;                                               // set base stamina to 100 (int)
        stamina_offset = 0.0f;                                          // set stamina offset to 0.0f (float), later to reset stamina off-time
    }

    // Update is called once per frame
    private void Update()
    {
        StaminaRegen();                             // Always check to see if we can regenerate players stamina
        PlayerMovement();                           // Calls PlayerMoment function, allows drift motion (left to right)
        PlayerSwim();                               // Calls PlayerSwim function, allows swimming motion (up)
        PlayerAttack();                             // Calls PlayerAttack function, 
        SetPlayerAnimation();                       // Calls SetPlayerAnimation function, 
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
        // move player right, changes swim animation, and rotations of player
        if (Input.GetKey(KeyCode.D))
        {
            current_animation = PlayerAnimation.Swim;
            transform.position = new Vector3(transform.position.x + (playerCamFollow.GetX()), transform.position.y, transform.position.z + (playerCamFollow.GetZ()));
            transform.rotation = Quaternion.Euler(0, playerCamFollow.GetRightRotation(), 0);
        }
        // move player left, changes swim animation, and rotations of player
        else if (Input.GetKey(KeyCode.A))
        {
            current_animation = PlayerAnimation.Swim;
            transform.position = new Vector3(transform.position.x + (-playerCamFollow.GetX()), transform.position.y, transform.position.z + (-playerCamFollow.GetZ()));
            transform.rotation = Quaternion.Euler(0, playerCamFollow.GetLeftRotation(), 0);
        }
        // When no input for movement is set, set current animation to idle
        else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && current_animation != PlayerAnimation.Attack) current_animation = PlayerAnimation.Idle;
    }

    /// <summary>
    /// PlayerSwim method, when space bar is pressed, player swims (repeated option)
    /// </summary>
    private void PlayerSwim()
    {
        // Player Jumps on space bar, as long as jumped == false, and boost_offset is less than a second
        if (Input.GetKeyDown(KeyCode.Space) && stamina > 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * swimVelocity; // Player Swim, uses Rigidbody, set for player's vector to swim up * swimvelocity
            stamina -= 3;                                                   // Reduce stamina upon swim physics
        }
    }

    /// <summary>
    /// PlayerAttack method, when a the desired key is pressed, set animation to attack, when it's let go, set to Idle
    /// </summary>
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            current_animation = PlayerAnimation.Attack;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            current_animation = PlayerAnimation.Idle;
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

    /// <summary>
    /// SetPlayerAnimation, controls which animation to be played next
    /// </summary>
    private void SetPlayerAnimation()
    {
        switch (current_animation)
        {
            // Idle happens when player is still, and on the ground
            case PlayerAnimation.Idle:
                anim.SetBool("attack", false);
                if (IsGrounded())
                {
                    anim.SetBool("swim_right", false);
                }
                break;
            case PlayerAnimation.Swim:
                anim.SetBool("swim_right", true);
                break;
            case PlayerAnimation.Attack:
                anim.SetBool("attack", true);
                break;
        }
    }

    /// <summary>
    /// GetPlayerStamina method: Returns the current stamina value
    /// </summary>
    /// <returns></returns>
    public float GetPlayerStamina()
    {
        return stamina;
    }

}
