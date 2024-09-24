using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // Cards available in the shop
    [SerializeField] private Object[] cardShop;
    [SerializeField] private int cardShopSize = 5;

    // Potential cards that could be in the shop
    [SerializeField] private Object[] cardPool;
    // Grants access to Object[] arrays allCards.numberCardPool and allCards.rareCardPool
    [SerializeField] private CardReferences allCards;
    
    // Until a proper area for player stuff exists
    [SerializeField] private int Chips = 500;

    // Waiting for items to be created

    // Start is called before the first frame update
    void Start()
    {
        cardShop = new CardController[cardShopSize];
        // Use allCards.numberCardPool and allCards.rareCardPool
        // cardPool = Resources.LoadAll("CardPool", typeof(CardController));

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
        for (int i = 0; i < cardShopSize; i++)
        {
            cardShop[i] = cardPool[Random.Range(0, cardPool.Length)];
        }
        Debug.Log("Shop randomized.");
    }
}
