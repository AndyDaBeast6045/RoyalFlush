using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private int playerMaxHealth;
    [SerializeField] private int playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    //
    public int GetHealth()
    {
        return playerCurrentHealth;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }
    //
    
    //
    public void Damage(int damage)
    {
        playerCurrentHealth -= damage;
    }

    public void Heal(int heal)
    {
        playerCurrentHealth += heal;
    }
    //

    public void Reset()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void Death()
    {
        Debug.Log("YOU DIED");
    }
}