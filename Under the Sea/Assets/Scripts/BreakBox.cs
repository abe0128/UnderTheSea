using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour
{
    public GameObject foodPrefab;

    public Rigidbody[] childArray;
	// Use this for initialization

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TailWhip"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<AudioSource>().Play();
            for(int i = 0; i < childArray.Length; i++)
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), childArray[i].GetComponent<MeshCollider>());
                
                childArray[i].GetComponent<Rigidbody>().useGravity = true;
                childArray[i].GetComponent<Rigidbody>().isKinematic = false;
                childArray[i].GetComponent<Rigidbody>().drag = 15f;
            }
            StartCoroutine(appearFood());
            Destroy(gameObject, 2.0f); 
        }
    }

    IEnumerator appearFood()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(foodPrefab, this.transform.position, this.transform.rotation);
    }
}
