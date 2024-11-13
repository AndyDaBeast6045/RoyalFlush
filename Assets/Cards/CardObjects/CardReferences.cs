using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReferences : MonoBehaviour
{
    [SerializeField] private CardObject[] spades;
    [SerializeField] private CardObject[] clubs;
    [SerializeField] private CardObject[] hearts;
    [SerializeField] private CardObject[] diamonds;

    public CardObject GetCard(CardObject.CardRank cardRank, CardObject.CardSuit cardSuit)
    {
        return GetRank(cardRank, GetSuit(cardSuit));
    }

    public CardObject[] GetSuit(CardObject.CardSuit cardSuit)
    {
        switch (cardSuit)
        {
            case CardObject.CardSuit.Spade:
                return spades;
            case CardObject.CardSuit.Heart:
                return hearts;
            case CardObject.CardSuit.Club:
                return clubs;
            case CardObject.CardSuit.Diamond:
                return diamonds;
            default: return null;
        }
    }

    public CardObject GetRank(CardObject.CardRank cardRank, CardObject[] suitArray)
    {
        switch (cardRank)
        {
            case CardObject.CardRank.Ace:
                return suitArray[0];
            case CardObject.CardRank.Two:
                return suitArray[1];
            case CardObject.CardRank.Three:
                return suitArray[2];
            case CardObject.CardRank.Four:
                return suitArray[3];
            case CardObject.CardRank.Five:
                return suitArray[4];
            case CardObject.CardRank.Six:
                return suitArray[5];
            case CardObject.CardRank.Seven:
                return suitArray[6];
            case CardObject.CardRank.Eight:
                return suitArray[7];
            case CardObject.CardRank.Nine:
                return suitArray[8];
            case CardObject.CardRank.Ten:
                return suitArray[9];
            case CardObject.CardRank.Jack:
                return suitArray[10];
            case CardObject.CardRank.Queen:
                return suitArray[11];
            case CardObject.CardRank.King:
                return suitArray[12];
            default:
                return null;
        }
    }
}
