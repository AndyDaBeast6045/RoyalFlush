using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private int chips = 0;

    void Start()
    {
        if (MainManager.Instance != null)
        {
            chips = MainManager.Instance.chips;
        }
    }

    // Used to show how many chips the player has in possession
    public int CheckChips()
    {
        Debug.Log("Chips: " + chips);
        return chips;
        
    }

    // Add chips to player's possession
    public void AddChips(int chips)
    {
        this.chips += chips;
        Debug.Log("Chips added: " + chips);
    }

    // Remove chips from player's possession
    // Change implementation for credit card later
    public void RemoveChips(int chips)
    {
        this.chips -= chips;
        Debug.Log("Chips removed: " + chips);
    }
}
