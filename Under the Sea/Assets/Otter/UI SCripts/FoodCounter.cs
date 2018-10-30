using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	private int count = 0;
	private int life = 5;
	public Text Life;
	public Text counter;
	// Update is called once per frame
	void Update () {
		
		count++;
		if (count >= 100) {
			//extra life
			if(life > 98){
				
			}else life++;
			//Debug.Log("Life: " + life);
			Life.text = life.ToString();

			count = 0;

		}
		counter.text = count.ToString();
		Debug.Log ("Collecting: " + count);
		
	}
}
