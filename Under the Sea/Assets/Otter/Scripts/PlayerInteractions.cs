using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private PlayerCamFollow playerCamFollow;  // To hold the script of 

    // Use this for initialization
    void Start () {
        playerCamFollow = GameObject.FindGameObjectWithTag("EventBus").GetComponent<PlayerCamFollow>(); // set script to the 
    }

    /// <summary>
    /// OnTriggerEnter Actions, For the following:
    /// 1. If it's object tagged TriggerRightRotation, call MoveCameraRight
    /// 2. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TriggerRightRotation")
        {
            playerCamFollow.MoveQuadrantRight();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "TriggerLeftRotation")
        {
            playerCamFollow.MoveQuadrantLeft();
            Destroy(other.gameObject);
        }
    }
}
