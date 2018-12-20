using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text lives_text;
    public Text food_text;
    public Text rock_count_text;
    public Image rock_image;
    public Image tail_image;
    public Image kelp_image;
    public Image background_image;
    public Image Stamina_bar;

    public GameObject food;
    public GameObject lives;
    public GameObject wheel;

    private PlayerController playerControl;
    

    private int food_counter;
    private int player_lives;
    private int current_weapon;
    private int rock_counter;

    private bool showui;

    private float wheel_timer;
    private float num;

    void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rock_counter = 5;
        food_counter = 0;
        player_lives = 5;
        current_weapon = 1;
        wheel_timer = 0;
        playerControl.GetComponent<Image>().fillAmount = .25f;
        rock_image.enabled = false;
        tail_image.enabled = false;
        kelp_image.enabled = false;
        food.SetActive(false);
        lives.SetActive(false);
        wheel.SetActive(false);
        showui = false;
        num = Stamina_bar.GetComponent<Image>().fillAmount;
    }

    void Update()
    {
        FoodCounter();
        WeaponWheel();
        Stamina_bar.GetComponent<Image>().fillAmount = playerControl.GetPlayerStamina();
        ShowUI();
        if (player_lives == 0)
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoadLevel("GameOverScene");
        }

    }

    private void ShowUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //sets the value
            if (showui == true)
            {
                showui = false;
            }

            else {
                showui = true;
            }

            //turns off or on the UI

            if (showui == true)
            {
                food.SetActive(true);
                lives.SetActive(true);
                wheel.SetActive(true);
            }
            else
            {
                food.SetActive(false);
                lives.SetActive(false);
                wheel.SetActive(false);
            }
        }
    }

    private void FoodCounter()
    {
        food_text.text = "x " + food_counter.ToString();
        lives_text.text = "x " + player_lives.ToString();
    }

    private void WeaponWheel()
    {
        if(current_weapon == 1)
        {
            tail_image.enabled = false;
            rock_image.enabled = true;
            kelp_image.enabled = false;
            rock_count_text.text = "x " + rock_counter;
        }

        wheel_timer++;
        if (Input.GetKeyDown(KeyCode.Space)) {
            wheel_timer = 0;
            wheel.SetActive(true);
            wheel_timer = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            wheel_timer = 0;
            wheel.SetActive(true);
            current_weapon++;

            if (current_weapon == 4)
            {
                current_weapon = 1;
            }
            switch (current_weapon)
            {
                case 1:
                    rock_image.enabled = true;
                    tail_image.enabled = false;
                    kelp_image.enabled = false;
                    rock_count_text.enabled = true;
                    rock_count_text.text = "x " + rock_counter;
                    break;
                case 2:
                    kelp_image.enabled = true;
                    rock_image.enabled = false;
                    tail_image.enabled = false;
                    rock_count_text.enabled = false;
                    break;
                case 3:
                    tail_image.enabled = true;
                    rock_image.enabled = false;
                    kelp_image.enabled = false;
                    rock_count_text.enabled = false;
                    break;
            }
            wheel_timer = 0;
        }
        
        else if (wheel_timer >= 120 && wheel_timer <= 125)
        {
            StartCoroutine(OverallTimer(1));

        }
    }

    public void IncrementFoodCount()
    {
        food.SetActive(true);
        food_counter++;
        if (food_counter > 99)
        {
            lives.SetActive(true);
            player_lives++;
            //StartCoroutine(OverallTimer(2));
            food_counter = 0;
        }
        //else
            //StartCoroutine(OverallTimer(1));
    }

    IEnumerator OverallTimer(int num)
    {
        yield return new WaitForSeconds(4);
        //food.SetActive (false);

        switch (num)
        {
            /*case 1:
                //for food
                food.SetActive(false);
                break;
            case 2:
                food.SetActive(false);
                lives.SetActive(false);
                //for lives
                break;*/
            case 1:
                //for weapon wheel
                wheel.SetActive(false);
                break;
            case 2:
                //display everything
                break;
        }
    }

    public void DecreaseRockCount()
    {
        if(rock_counter > 0)
        {
            rock_counter--;
        }
    }

    public void IncreaseRockCount()
    {
        if(rock_counter < 5)
        {
            rock_counter++;
        }
    }

    public int getRockCount()
    {
        return rock_counter;
    }

    public int getCurrentWeapon()
    {
        return current_weapon;
    }

    public void LoseLife()
    {
        player_lives--;
    }
    

}

