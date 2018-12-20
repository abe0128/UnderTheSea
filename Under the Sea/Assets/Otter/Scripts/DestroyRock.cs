using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRock : MonoBehaviour {
    private float distanceToGround;     // To hold the current disntance from ground

    void Start() {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;   // set value to to the bottom of the object
    }

    private void FixedUpdate()
    {
        // If ball hits ground, start destroy function after 2 seconds
        // Destroys rock
        if (IsGrounded())
        {
            Destroy(gameObject, 2f);
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
}
