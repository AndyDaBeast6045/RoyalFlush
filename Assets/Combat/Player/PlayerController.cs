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
    [SerializeField] private int playerShield;
    [SerializeField] private int burnCount;
    [SerializeField] private int isDead;

    //Internal References
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text healthBarValue;

    [SerializeField] private GameObject shieldBar;
    [SerializeField] private TMP_Text shieldBarValue;

    [SerializeField] private GameObject burnBar;
    [SerializeField] private TMP_Text burnBarValue;

    [SerializeField] private Animator objectAnimator;

    //External References
    private CombatController combatControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        isDead = 0;
        combatControllerObject = GameObject.FindWithTag("CombatController").GetComponent<CombatController>();
        
        if (MainManager.Instance != null)
        {
            playerMaxHealth = MainManager.Instance.playerMaxHealth;
            playerCurrentHealth = MainManager.Instance.playerCurrentHealth;
        }
        UpdateHealthBar();
    }
    void Update()
    {
        if (isDead == 1)
        {
            Debug.Log("Player has died.");
            combatControllerObject.Defeat();
            Destroy(gameObject);
        }
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

    public void Burn()
    {
        Damage(burnCount);
        burnCount /= 2;
        UpdateHealthBar();
    }

    public void Damage(int damage)
    {
        if (damage > 0)
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
                objectAnimator.SetTrigger("DeathTrigger");
            }
            else
            {
                objectAnimator.SetTrigger("Damaged");
                UpdateHealthBar();
            }
        }
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
            shieldBar.GetComponent<Slider>().value = ((float)playerShield / (float)playerMaxHealth/2);
            shieldBarValue.text = playerShield.ToString();
        }
        else
        {
            shieldBar.SetActive(false);
        }
        if (burnCount > 0)
        {
            burnBar.SetActive(true);
            burnBarValue.text = burnCount.ToString();
        }
        else
        {
            burnBar.SetActive(false);
        }
    }
}
