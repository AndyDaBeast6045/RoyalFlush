using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private int playerMaxHealth;
    [SerializeField] private int playerCurrentHealth;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text healthBarValue;
    [SerializeField] private int playerShield;
    [SerializeField] private GameObject shieldBar;
    [SerializeField] private TMP_Text shieldBarValue;

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance != null)
        {
            playerMaxHealth = MainManager.Instance.playerMaxHealth;
            playerCurrentHealth = MainManager.Instance.playerCurrentHealth;
        }
        UpdateHealthBar();
    }

    //
    public int GetHealth()
    {
        return playerCurrentHealth;
    }

    public void ResetShield()
    {
        playerShield = 0;
        UpdateHealthBar();
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }
    //
    
    //
    public void Damage(int damage)
    {
        if (playerShield > 0)
        {
            int damageReduced = playerShield;
            playerShield -= damage;
            damage -= damageReduced;
            if (damage < 0)
            {
                damage = 0;
            }
            if (playerShield < 0)
            {
                playerShield = 0;
            }
        }
        playerCurrentHealth -= damage;
        if (playerCurrentHealth <= 0)
        {
            Death();
        }
        UpdateHealthBar();
    }

    public void Heal(int heal)
    {
        playerCurrentHealth += heal;
        UpdateHealthBar();
    }
    //

    public void Reset()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void Shield(int shield)
    {
        playerShield += shield;
    }

    public void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = ((float)playerCurrentHealth / (float)playerMaxHealth);
        healthBarValue.text = playerCurrentHealth + " / " + playerMaxHealth;
        if (playerShield > 0)
        {
            shieldBar.SetActive(true);
            shieldBar.GetComponent<Slider>().value = ((float)playerShield / (float)playerMaxHealth);
            shieldBarValue.text = playerShield.ToString();
        }
        else
        {
            shieldBar.SetActive(false);
        }
    }

    public void Death()
    {
        Debug.Log("YOU DIED");
    }
}
