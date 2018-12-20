using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour
{
    public GameObject foodPrefab;   // To hold the prefab of the game object, later to be instantiated
    public Rigidbody[] childArray;  // To hold an array of rigid body objects
	// Use this for initialization

    /// <summary>
    /// OnCollisionEnter method, for the following tasks
    /// 1. If object is tagged as Player, and also while on collision the player's animation is the same as a TailWhip, do the following
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // 1.
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TailWhip"))
        {
            GetComponent<BoxCollider>().enabled = false;    // disable the box collider to the original box.
            GetComponent<AudioSource>().Play();             // play the audio source of the box breaking.

            // For loop, loops through the array of rigid body, contains the children of the box (pre-broken parts)
            for(int i = 0; i < childArray.Length; i++)
            {
                // Upon break, player ignores all of the colliders of the children
                Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), childArray[i].GetComponent<MeshCollider>());
                
                childArray[i].GetComponent<Rigidbody>().useGravity = true;      // turn on gravity on the childs rigidbody, for explosion effect
                childArray[i].GetComponent<Rigidbody>().isKinematic = false;    // turn off kinamatic, so it object can move out of box
                childArray[i].GetComponent<Rigidbody>().drag = 15f;             // set drag to each child to 15, give an underwater appearance of breaking
            }
            StartCoroutine(appearFood());   // start a corutine, makes a food object appear in the same position where the box was at
            Destroy(gameObject, 2.0f);      // Destroy box, and children of box after 2 seconds
        }
    }

    /// <summary>
    /// appearFood() method, instantiates a food object at this.position, and this.rotation
    /// </summary>
    /// <returns></returns>
    IEnumerator appearFood()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(foodPrefab, this.transform.position, this.transform.rotation);
    }
}
