using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopHeal : MonoBehaviour
{
    [SerializeField] private TMP_Text currentHealthDisplay;
    [SerializeField] private TMP_Text healCostDisplay;
    private int healCost;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthDisplay.text = "Current Health: " + MainManager.Instance.playerCurrentHealth + " / " + MainManager.Instance.playerMaxHealth;
        healCost = (MainManager.Instance.playerMaxHealth - MainManager.Instance.playerCurrentHealth) * 3;
        healCostDisplay.text = healCost.ToString();
    }

    public void Heal()
    {
        if (MainManager.Instance.chips >= healCost)
        {
            MainManager.Instance.playerCurrentHealth = MainManager.Instance.playerMaxHealth;
            MainManager.Instance.chips -= healCost;
            Destroy(gameObject);
        }
    }
}
