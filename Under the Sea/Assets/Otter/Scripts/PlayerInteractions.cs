using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerCamFollow playerCamFollow;    // To hold the script of PlayerCamFollow
    private PlayerController playerController;  // To hold the script of PlayerController
    private PlayerUI playerUI;                  // To hold the script of PlayerUI
    private AudioSource audioSource;
    private Animator anim;                      // To hold the animator component
    private GameObject cam;                     // To hold the camer game object
    private int quadrant;                       // To hold the quadrant per checkpoint

    // Use this for initialization
    void Start()
    {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>(); // set script to the PlayerCamFollow
        playerController = GetComponent<PlayerController>();                                            // set script to the PlayerController
        playerUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<PlayerUI>();   // set script to the PlayerUI
        cam = GameObject.FindGameObjectWithTag("MainCamera");                                           // set camera game object to cam
        quadrant = playerCamFollow.GetQuadrant();                                                       // set the current quadrant in PlayerCamFollow to quadrant
        anim = GetComponent<Animator>();                                                                // set the animator coponent to anim
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// OnTriggerEnter Actions, For the following:
    /// 1. If it's object tagged TriggerRightRotation, call MoveCameraRight, and start corutine
    /// 2. If it's object tagged TriggerLeftRotation, call MoveCameraLeft, and start corutine
    /// 3. If it's object tagged Rock, collect the rock into your inventory
    /// 4. If it's object tagged Pearl, change empty objects transforms to player and cam current position,
    ///     plus, gets the current quadrant in PlayerCamFollow (get's movement direction plus rotation settings)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {   // 1.
        if (other.gameObject.tag == "TriggerRightRotation")
        {
            playerCamFollow.MoveQuadrantRight();
            StartCoroutine(ActiveRotations(other.gameObject));
        }
        // 2.
        if (other.gameObject.tag == "TriggerLeftRotation")
        {
            playerCamFollow.MoveQuadrantLeft();
            StartCoroutine(ActiveRotations(other.gameObject));
        }
        // 3.
        if (other.gameObject.tag == "Rock")
        {
            playerUI.IncreaseRockCount();
            Destroy(other.gameObject);
        }
        // 4.
        if (other.gameObject.tag == "Pearl")
        {
            playerCamFollow.spawnLocation.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            playerCamFollow.camLocation.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            quadrant = playerCamFollow.GetQuadrant();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Food")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
            playerUI.IncrementFoodCount();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    /// <summary>
    /// OnCollisionEnter Actions, For the following:
    /// 1. If the object is tagged "Food", call PlayerUI to increment food count by 1, destroy object on collision
    /// 2. If the object is tagged "Enemy", Start a corutine for relocating player, as long as player has lives remaining.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // 1.
        if (collision.gameObject.tag == "Food")
        {
            playerUI.IncrementFoodCount();
            audioSource.clip = playerController.audioClips[3];
            audioSource.Play();
            Destroy(collision.gameObject);
        }
        // 2.
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(RelocatePlayer());
            playerUI.LoseLife();
        }
    }
    /// <summary>
    /// ActiveRotation corutine, Activates and deactivates the trigger rotatations.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator ActiveRotations(GameObject go)
    {
        go.SetActive(false);
        yield return new WaitForSeconds(2f);
        go.SetActive(true);
    }

    /// <summary>
    /// RelocatePlayer Corutine:
    /// 1. Plays Death Animation
    /// 2. Stops player's movement for 2 seconds, if and only if, player still has lives
    /// 3. After the 2 seconds, Starts Players's Movement
    /// 4. Plays Idle Animation
    /// 5. Relocates the player to last checkpoint.
    /// 6. Resets the quadrant according to the checkpoint.
    /// </summary>
    /// <returns></returns>
    IEnumerator RelocatePlayer()
    {
        anim.Play("Death");
        playerController.SetIfDead();
        yield return new WaitForSeconds(4f);
        playerController.SetIfDead();
        anim.Play("Rac_Swim Idle");
        transform.position = playerCamFollow.spawnLocation.position;
        cam.transform.position = playerCamFollow.camLocation.position;
        playerCamFollow.SetQuadrant(quadrant);
    }
}
