using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private Transform normalShop;

    // Cards available in the shop
    [SerializeField] private Object[] normalCardShop;
    [SerializeField] private Object[] rareCardShop;
    //[SerializeField] private Object[] itemShop;
    [SerializeField] private int normalCardShopSize;
    [SerializeField] private int rareCardShopSize;
    //[SerializeField] private int itemShopSize = 3;

    // Grants access to Object[] arrays allCards.numberCardPool and allCards.rareCardPool
    [SerializeField] private CardReferences allCards;

    [SerializeField] private DeckController deck;

    // Start is called before the first frame update
    void Start()
    {
        normalCardShop = new Object[normalCardShopSize];
        rareCardShop = new Object[rareCardShopSize];
        EnterShop();
    }

    // Activates when the player enters a shop node
    public void EnterShop()
    {
        randomize();
        for (int i = 0; i < normalCardShopSize; i++)
        {
            GameObject ShopItem = Instantiate(shopCardPrefab, normalShop);
            //button = ShopItem.GetComponent<Button>();
        }
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
