using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // Cards available in the shop
    [SerializeField] private Object[] normalCardShop;
    [SerializeField] private Object[] rareCardShop;
    //[SerializeField] private Object[] itemShop;
    [SerializeField] private int normalCardShopSize = 4;
    [SerializeField] private int rareCardShopSize = 1;
    //[SerializeField] private int itemShopSize = 3;

    // Grants access to Object[] arrays allCards.numberCardPool and allCards.rareCardPool
    [SerializeField] private CardReferences allCards;

    // Start is called before the first frame update
    void Start()
    {
        normalCardShop = new Object[normalCardShopSize];
        rareCardShop = new Object[rareCardShopSize];
        randomize();
    }

    // Activates when the player enters a shop node
    public void EnterShop()
    {
        randomize();
    }

    // Randomizes the contents of the shop
    private void randomize()
    {
        for (int i = 0; i < normalCardShopSize; i++)
        {
            normalCardShop[i] = allCards.numberCardPool[Random.Range(0, allCards.numberCardPool.Length)];
        }
        for (int i = 0; i < rareCardShopSize; i++)
        {
            rareCardShop[i] = allCards.rareCardPool[Random.Range(0, allCards.rareCardPool.Length)];
        }
        Debug.Log("Shop randomized.");
    }
}
