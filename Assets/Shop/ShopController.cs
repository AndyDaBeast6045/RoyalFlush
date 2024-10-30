using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Transform normalShop;

    //Pool of cards to be selected for randomization
    [SerializeField] private Object[] normalCardPool;
    [SerializeField] private Object[] rareCardPool;
    [SerializeField] private Object[] itemPool;

    // Cards available in the shop
    [SerializeField] private Object[] normalCardShop;
    [SerializeField] private Object[] rareCardShop;
    [SerializeField] private Object[] itemShop;

    [SerializeField] private int normalCardShopSize;
    [SerializeField] private int rareCardShopSize;
    [SerializeField] private int itemShopSize;

    // Start is called before the first frame update
    void Start()
    {
        normalCardPool = Resources.LoadAll("Shop/ShopNormal", typeof(Object));
        rareCardPool = Resources.LoadAll("Shop/ShopRare", typeof(Object));
        itemPool = Resources.LoadAll("Shop/Item", typeof(Object));

        normalCardShop = new Object[normalCardShopSize];
        rareCardShop = new Object[rareCardShopSize];
        itemShop = new Object[itemShopSize];
        EnterShop();
    }

    // Activates when the player enters a shop node
    public void EnterShop()
    {
        randomizeAll();
        instantiateAll();
    }

    private void instantiateAll()
    {
        instantiateNormalShop();
    }

    private void instantiateNormalShop()
    {
        for (int i = 0; i < normalCardShopSize; i++)
        {
            Instantiate(normalCardShop[i], normalShop);
        }
        Debug.Log("Normal Shop instantiated.");
    }

    // Randomizes the contents of the shop
    private void randomizeAll()
    {
        randomizeNormalShop();
        //randomizeRareShop();
    }

    private void randomizeNormalShop()
    {
        for (int i = 0; i < normalCardShopSize; i++)
        {
            normalCardShop[i] = normalCardPool[Random.Range(0, normalCardPool.Length)];
        }
        Debug.Log("Normal Shop randomized.");
    }

    private void randomizeRareShop()
    {
        for (int i = 0; i < rareCardShopSize; i++)
        {
            rareCardShop[i] = rareCardPool[Random.Range(0, rareCardPool.Length)];
        }
        Debug.Log("Rare Shop randomized.");
    }
}
