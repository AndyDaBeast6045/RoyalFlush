using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth;
    [SerializeField] private int playerCurrentHealth;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private bool alive;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public int GetHealth()
    {
        return playerCurrentHealth;
    }

    public void Damage(int damage)
    {
        playerCurrentHealth -= damage;
    }

    public void Heal(int heal)
    {
        playerCurrentHealth += heal;
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().sprite = playerSprite;
        alive = true;
        playerCurrentHealth = playerMaxHealth;
    }

    public void Death()
    {
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        Debug.Log("YOU DIED");
    }
}
