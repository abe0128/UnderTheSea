using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour {
	//arsenal = weapon in your disposal
	private int arsenal_counter;
	public Text arsenal;

	// Use this for initialization
	void Start () {
		arsenal_counter = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			arsenal_counter++;
		
			if(arsenal_counter == 4){
				arsenal_counter = 1;
			}
		}
		switch (arsenal_counter) {
		case 1:
			arsenal.text = "Rock";
			break;
		case 2:
			arsenal.text = "KelpWhhip";
			break;
		case 3:
			arsenal.text = "TailWhhip";
			break;
		}
	}
}
