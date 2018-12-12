using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour
{

    public Rigidbody[] childArray;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TailWhip"))
        {
            
            for(int i = 0; i < childArray.Length; i++)
            {
                Debug.Log(childArray[i]);
                childArray[i].GetComponent<Rigidbody>().useGravity = true;
                childArray[i].GetComponent<Rigidbody>().isKinematic = false;
                
            }
            Destroy(gameObject, 1f); 
        }
    }
}
