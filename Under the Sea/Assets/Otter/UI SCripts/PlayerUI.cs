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
    private int food_counter;
    private int player_lives;
    private int current_weapon;
    private int rock_counter;


    void Start()
    {
        rock_counter = 5;
        food_counter = 0;
        player_lives = 5;
        current_weapon = 1;
        rock_image.enabled = false;
        tail_image.enabled = false;
        kelp_image.enabled = false;
    }

    void Update()
    {
        FoodCounter();
        WeaponWheel();
    }

    private void FoodCounter()
    {
        if (food_counter > 100)
        {
            player_lives++;
            food_counter = 0;
        }
        food_text.text = "x " + food_counter.ToString();
        lives_text.text = "x " + player_lives.ToString();
    }

    private void WeaponWheel()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            current_weapon++;

            if (current_weapon == 4)
            {
                current_weapon = 1;
            }
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

    public void IncrementFoodCount()
    {
        food_counter++;
    }
}
