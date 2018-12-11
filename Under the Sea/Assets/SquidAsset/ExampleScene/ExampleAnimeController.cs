using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleAnimeController : MonoBehaviour {

    Animator anime;

    void Awake()
    {
        anime = FindObjectOfType<Animator>();
    }
    void Start()
    {
        anime.SetTrigger("Idle");
    }
    public void PlayAnime(string _triggerID)
    {
        anime.SetTrigger(_triggerID);
    }
	
}
