using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip[] audioClips;              // To hold an array of different audios, later to use for player's audio options
    public GameObject kelpWhip;                 // To hold the kelpwhip object, later to turn on and off
    public Rigidbody rockPrefab;                // To hold the prefab of the rock
    public Transform leftHand;                  // To hold the transform from where rock will shoot out from
    public float swimVelocity;                  // Changeable value, swim velocity

    private AudioSource audioSource;            // To hold the players AudioSource component
    private PlayerAnimation current_animation;  // To hold the current otter animation
    private PlayerCamFollow playerCamFollow;    // To hold the script of the 
    private PlayerUI playerUI;                  // To hold the script of the PlayerUI
    private Animator anim;                      // To hold the otter animator, (which animation to play)
    private float stamina;                      // To hold the player's stamina
    private float distanceToGround;             // To hold the current disntance from ground
    private float stamina_offset;               // To hold the countdown to stamina offset
    private float rock_force;                   // To hold the force that the rock is being thrown
    private bool throwRight;                    // To hold the direction in which the otter is facing, used for getting direction in rock throw
    public bool isDead;                         // To hold a boolean value, later to be used to tell if player dies
    public bool isAttacking;                    // To hold a boolean value, later to be used to tell if the player is attacking

    /// <summary>
    /// PlayerAnimation: Enum operator, For player animation
    /// 1. Idle: Animation for no movement
    /// 2. Swim: Animation for swimming (left and right movement)
    /// 3. Attack: Animation for rock throw attack
    /// 4. KelpWhip: Animation for attack is performed, same as rock, but sets kelp active or inactive
    /// </summary>
    private enum PlayerAnimation
    {
        Idle,
        Swim,
        Attack,
        TailWhip
    }

    // Use this for initialization
    void Start()
    {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>(); // Holds PlayerCamFollow script
        playerUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<PlayerUI>();   // Holds PlayerUI script
        audioSource = GetComponent<AudioSource>();                      // set AudioSource component to audioSource
        current_animation = PlayerAnimation.Idle;                       // set current animation to Idle
        anim = GetComponent<Animator>();                                // set Animator component to anim
        distanceToGround = GetComponent<Collider>().bounds.extents.y;   // set value to to the bottom of the object
        stamina = .25f;                                                 // set base stamina to 100 (int)
        stamina_offset = 0.0f;                                          // set stamina offset to 0.0f (float), later to reset stamina off-time
        rock_force = 750f;                                              // set force of throw to 750
        throwRight = true;                                              // set direction to starting facing direction, they're set in PlayerMovement function
        isDead = false;                                                 // set boolean value of isDead to false, player is alive at start
        isAttacking = false;                                            // set boolean value of isAttacking to false, player starts game at idle, and is not attacking
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isDead)
        {
            StaminaRegen();                             // Always check to see if we can regenerate players stamina
            PlayerMovement();                           // Calls PlayerMoment function, allows drift motion (left to right)
            PlayerSwim();                               // Calls PlayerSwim function, allows swimming motion (up)
            PlayerAttack();                             // Calls PlayerAttack function, 
            SetPlayerAnimation();                       // Calls SetPlayerAnimation function, 
            GetComponent<Rigidbody>().drag = 4;         // Set player's drag to four, from Ridigbody component (illusion of underwater)
        }
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
            throwRight = true;
            transform.position = new Vector3(transform.position.x  + (playerCamFollow.GetX()), transform.position.y, transform.position.z + (playerCamFollow.GetZ()));
            transform.rotation = Quaternion.Euler(0, playerCamFollow.GetRightRotation(), 0);
        }
        // move player left, changes swim animation, and rotations of player
        else if (Input.GetKey(KeyCode.A))
        {
            current_animation = PlayerAnimation.Swim;
            throwRight = false;
            transform.position = new Vector3(transform.position.x + (-playerCamFollow.GetX()), transform.position.y, transform.position.z + (-playerCamFollow.GetZ()));
            transform.rotation = Quaternion.Euler(0, playerCamFollow.GetLeftRotation(), 0);
        }
        // When no input for movement is set, set current animation to idle
        else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))) current_animation = PlayerAnimation.Idle;
    }

    /// <summary>
    /// PlayerSwim method, when space bar is pressed, player swims (repeated option)
    /// </summary>
    private void PlayerSwim()
    {
        // Player Jumps on space bar, as long as jumped == false, and boost_offset is less than a second
        if (Input.GetKeyDown(KeyCode.Space) && stamina > 0 && transform.position.y < 8f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * swimVelocity; // Player Swim, uses Rigidbody, set for player's vector to swim up * swimvelocity
            stamina -= 0.003f;                                                   // Reduce stamina upon swim physics
        }
    }

    /// <summary>
    /// PlayerAttack method, when a the desired key is pressed, set animation to attack, when it's let go, set to Idle
    /// </summary>
    private void PlayerAttack()
    {
        // If mouse is clicked, Starts an attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            // Switch statement: Get's current weapon in weapon wheel (PlayerUI Script)
            switch (playerUI.getCurrentWeapon())
            {
                // Case for rock
                case 1:
                    // Make sure player has rock count
                    if (playerUI.getRockCount() > 0)
                    {
                        StartCoroutine("ThrowRockTimer");
                    }
                    break;
                // Case for kelp whip
                case 2:
                    StartCoroutine(KelpWhipTimer());
                    break;
                // Case for tail whip
                case 3:
                    // set attackin as true
                    isAttacking = true;

                    // When the audio source of tail whip is not playing
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = audioClips[2];
                        audioSource.Play();
                        anim.Play("TailWhip");
                        
                    }
                    // set back to false
                    isAttacking = false;
                    break;
            }
        }
        // goes back to idle animation
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
        stamina = (stamina > .25f) ? .25f : stamina;  // ternary operator, Stamina can't exceed 100
    }

    /// <summary>
    /// SetPlayerAnimation, controls which animation to be played next
    /// </summary>
    private void SetPlayerAnimation()
    {
        switch (current_animation)
        {
            // Idle happens when player is still, and on the ground, swim starts on momevment
            case PlayerAnimation.Idle:
                if (IsGrounded())
                {
                    anim.SetBool("swim_right", false);
                }
                break;
            case PlayerAnimation.Swim:
                anim.SetBool("swim_right", true);
                break;
        }
    }

    // IEnumerator, Activate the kelp just as long as the attack animation, set it inactive after 1 sec.
    // set attack to true, activate kelp object, check for audio source, wait one second, de-activate kelp, and set isAttacking to false.
    IEnumerator KelpWhipTimer()
    {
        isAttacking = true;
        kelpWhip.SetActive(true);

        // When the audio source of kelp whip is not playing
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            anim.Play("Attack");
        }
        
        yield return new WaitForSeconds(1f);
        kelpWhip.SetActive(false);
        isAttacking = false;
    }

    // IEnumerator, To get rock to get thrown right as the animation looks as it's throwing the rock
    // set attack to true, check for audiosource play, wait .4f seconds, start ThrowRock function, after set isAttacking to false.
    IEnumerator ThrowRockTimer()
    {
        isAttacking = true;

        // When the audio source of rock is not playing
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            anim.Play("Attack");
        }
        yield return new WaitForSeconds(.4f);
        ThrowRock();
        isAttacking = false;
    }

    /// <summary>
    /// ThrowRock, Starts a process of instantiating the rockPrefab, and forces it in the direction the otter is facing
    /// </summary>
    private void ThrowRock()
    {
        // Instantiate rock and save it into rockClone
        Rigidbody rockClone = Instantiate(rockPrefab, leftHand.position, leftHand.rotation) as Rigidbody;

        // If otter is looking right, throw rock to the right, else, throw rock to the left
        if (throwRight)
        {
            rockClone.AddForce(new Vector3(playerCamFollow.GetX() * 20, 0, playerCamFollow.GetZ() * 20) * rock_force); // 0.05 * 20 = 1
        }
        else
        {
            rockClone.AddForce(new Vector3(-playerCamFollow.GetX() * 20, 0, -playerCamFollow.GetZ() * 20) * rock_force); // 0.05 * 20 = 1
        }
        playerUI.DecreaseRockCount();
    }

    /// <summary>
    /// GetPlayerStamina method: Returns the current stamina value
    /// </summary>
    /// <returns></returns>
    public float GetPlayerStamina()
    {
        return stamina;
    }

    // Setting dead animation on and off
    public void SetIfDead()
    {
        isDead = !isDead;
    }

    // going back to idle
    public void SetIdle()
    {
        current_animation = PlayerAnimation.Idle;
    }

}
