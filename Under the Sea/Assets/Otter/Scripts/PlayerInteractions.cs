using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private PlayerCamFollow playerCamFollow;    // To hold the script of PlayerCamFollow
    private PlayerUI playerUI;                  // To hold the script of PlayerUI

    // Use this for initialization
    void Start()
    {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>(); // set script to the PlayerCamFollow
        playerUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<PlayerUI>();   // set script to the PlayerUI
    }

    /// <summary>
    /// OnTriggerEnter Actions, For the following:
    /// 1. If it's object tagged TriggerRightRotation, call MoveCameraRight
    /// 2. If it's object tagged TriggerLeftRotation, call MoveCameraLeft
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {   // 1.
        if (other.gameObject.tag == "TriggerRightRotation")
        {
            playerCamFollow.MoveQuadrantRight();
            Destroy(other.gameObject);
        }
        // 2.
        if (other.gameObject.tag == "TriggerLeftRotation")
        {
            playerCamFollow.MoveQuadrantLeft();
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// OnCollisionEnter Actions, For the following:
    /// 1. If the object is tagged "Food", call PlayerUI to increment food count by 1, destroy object on collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // 1.
        if (collision.gameObject.tag == "Food")
        {
            playerUI.IncrementFoodCount();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Rock")
        {
            playerUI.IncreaseRockCount();
            Destroy(collision.gameObject);
        }
    }
}
