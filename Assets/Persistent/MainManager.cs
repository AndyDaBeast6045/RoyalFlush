using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int chips;
    public int playerCurrentHealth;
    public int playerMaxHealth;
    public int playerHandSize;
    public int nextEncounter;
    public List<CardObject> deck;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void changeChips(int chipChange)
    {
        chips += chipChange;
        if (chips < 0)
        {
            chips = 0;
        }
    }

    public void changeHealth(int health)
    {
        playerCurrentHealth += health;
        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }

    public void loseACardFromDeck()
    {
        int randomNum = Random.Range(0, deck.Count);
        deck.RemoveAt(randomNum);
    }

    //Adds a random card to the player's deck
    public void gainACard()
    {
        //uhhhhhhh
    }

    public void changeEncounter(int num)
    {
        nextEncounter = num;
    }

    public int getHP()
    {
        return playerCurrentHealth;
    }

    public int getChips()
    {
        return chips;
    }
}
