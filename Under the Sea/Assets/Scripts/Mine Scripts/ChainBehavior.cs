using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Matthew Connolly
// This attaches the chains together to form a chain type movement with the mine.
public class ChainBehavior : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
