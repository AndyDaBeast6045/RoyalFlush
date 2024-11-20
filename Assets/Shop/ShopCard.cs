using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCard : MonoBehaviour
{
    private CardObject shopItem;
    private int cost;
    private CardReferences cardReferences;
    [SerializeField] private TMP_Text costDisplay;
    [SerializeField] private Image card1;
    [SerializeField] private Image card2;
    [SerializeField] private Image card3;
    [SerializeField] private Image card4;
    [SerializeField] private Image card5;
    [SerializeField] private GameObject entireStack;

    void Start()
    {
        cardReferences = GameObject.FindWithTag("CardReferences").GetComponent<CardReferences>();
        shopItem = cardReferences.GetRandomCard();
        switch (shopItem.GetRank())
        {
            case CardObject.CardRank.Ace:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Two:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Three:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Four:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Five:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Six:
                cost = Random.Range(150, 350);
                break;
            case CardObject.CardRank.Seven:
                cost = Random.Range(350, 500);
                break;
            case CardObject.CardRank.Eight:
                cost = Random.Range(350, 500);
                break;
            case CardObject.CardRank.Nine:
                cost = Random.Range(350, 500);
                break;
            case CardObject.CardRank.Ten:
                cost = Random.Range(350, 500);
                break;
            case CardObject.CardRank.Jack:
                cost = Random.Range(500, 600);
                break;
            case CardObject.CardRank.Queen:
                cost = Random.Range(500, 600);
                break;
            case CardObject.CardRank.King:
                cost = Random.Range(500, 600);
                break;
            default:
                break;
        }
        costDisplay.text = cost.ToString();
        card1.sprite = shopItem.GetSprite();
        card2.sprite = shopItem.GetSprite();
        card3.sprite = shopItem.GetSprite();
        card4.sprite = shopItem.GetSprite();
        card5.sprite = shopItem.GetSprite();
    }

    public void Buy()
    {
        if (MainManager.Instance.chips >= cost)
        {
            MainManager.Instance.chips -= cost;
            MainManager.Instance.deck.Add(shopItem);
            MainManager.Instance.deck.Add(shopItem);
            MainManager.Instance.deck.Add(shopItem);
            MainManager.Instance.deck.Add(shopItem);
            MainManager.Instance.deck.Add(shopItem);
            Destroy(entireStack);
        }
    }
}
