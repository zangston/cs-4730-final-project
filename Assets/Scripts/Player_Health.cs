using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    public Image healthBarFill;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        
    }
    public void TakeDamage(int damage)
    {
        Player_Movement player = GetComponent<Player_Movement>();
        if (player != null && !player.InvincibleCheck())
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth == 0)
            {
                SceneManager.LoadScene("GameOver");
            }

            UpdateHealthBar();

        }
    }
    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        healthBarFill.fillAmount = healthPercentage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart"))
        {
            // Gain 20 health
            GainHealth(20);  
            // Delete heart after collision
            Destroy(collision.gameObject);  
        }
    }


    public void GainHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't exceed max
        UpdateHealthBar();
    }


    // Update is called once per frame
    void Update()
    {
        // Verify that TakeDamage works
        //  if (Input.GetKeyDown(KeyCode.D))
        // {
        //     TakeDamage(10);
        // }

    }
}
