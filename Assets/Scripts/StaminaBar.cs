using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Player_Movement player;  // Public reference to the Player_Movement script
    private Image staminaBarImage;  // Reference to the Image component

    void Start()
    {
        staminaBarImage = GetComponent<Image>();
    }

    void Update()
    {
        if (player != null)
        {
            // Update the fill amount based on player's current stamina
            staminaBarImage.fillAmount = player.CurrentStamina / player.MaxStamina;
        }
    }
}

