using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Deck/Card", order = 0)]
public class CardObject : ScriptableObject
{
    //Enums
    public enum CardRank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
    public enum CardSuit { Club, Heart, Spade, Diamond };

    //Internal Variables
    [SerializeField] private CardRank cardRank;
    [SerializeField] private CardSuit cardSuit;
    [SerializeField] private string cardName;
    [SerializeField] private Sprite cardSprite;

    public string GetName()
    {
        return cardName;
    }

    public CardRank GetRank()
    {
        return cardRank;
    }

    public CardSuit GetSuit()
    {
        return cardSuit;
    }

    public Sprite GetSprite()
    {
        return cardSprite;
    }

    
    public void changeSuit(CardSuit newSuit)
    {
        cardSuit = newSuit;

    }
}
