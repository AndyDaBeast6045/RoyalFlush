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

    public CardObject GetRandomCard()
    {
        return GetRank(GetRandomRank(), GetRandomSuit());
    }

    public CardObject[] GetRandomSuit()
    {
        int suit = Random.Range(0, 4);
        switch (suit)
        {
            case 0:
                return spades;
            case 1:
                return clubs;
            case 2:
                return hearts;
            case 3:
                return diamonds;
            default:
                return null;
        }
    }
    public CardObject.CardRank GetRandomRank()
    {
        int rank = Random.Range(0, 13);
        switch (rank)
        {
            case 0:
                return CardObject.CardRank.Ace;
            case 1:
                return CardObject.CardRank.Two;
            case 2:
                return CardObject.CardRank.Three;
            case 3:
                return CardObject.CardRank.Four;
            case 4:
                return CardObject.CardRank.Five;
            case 5:
                return CardObject.CardRank.Six;
            case 6:
                return CardObject.CardRank.Seven;
            case 7:
                return CardObject.CardRank.Eight;
            case 8:
                return CardObject.CardRank.Nine;
            case 9:
                return CardObject.CardRank.Ten;
            case 10:
                return CardObject.CardRank.Jack;
            case 11:
                return CardObject.CardRank.Queen;
            case 12:
                return CardObject.CardRank.King;
            default:
                return CardObject.CardRank.Ace;
        }
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
            default: 
                return null;
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
