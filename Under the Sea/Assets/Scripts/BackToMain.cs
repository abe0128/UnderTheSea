using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {
    private LevelManager levelManager;  // To hold the level manager script
    
	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();   // set level manager script to levelManager
        StartCoroutine(ToMain());                                                                       // start a coroutine, to load back to main scene after 5 sec
	}

    /// <summary>
    /// ToMain() method, returns scene to TitleScene after 5 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator ToMain()
    {
        yield return new WaitForSeconds(5f);
        levelManager.LoadLevel("TitleScene");
    }
	
}
