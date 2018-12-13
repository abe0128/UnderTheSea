using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {
    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        StartCoroutine(ToMain());
	}

    IEnumerator ToMain()
    {
        yield return new WaitForSeconds(5f);
        levelManager.LoadLevel("TitleScene");
    }
	
}
