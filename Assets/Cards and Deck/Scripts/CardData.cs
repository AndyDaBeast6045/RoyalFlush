using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// To create a new CardData asset: right click in project window > Create > New Card, then fill in the data.

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject, IComparable
{
    public enum CardSuit {Clubs, Diamonds, Hearts, Spades};

    [SerializeField] private string cardName;
    [SerializeField] private Sprite sprite;

    [SerializeField] private char rank; // T represents 10
    [SerializeField] private CardSuit suit; 
    [SerializeField] public CardScript cardScript;
    [SerializeField] private bool isRare; // Mark true if rank is J, Q, K, or A
    

    public Sprite GetSprite() { return sprite; }
    public char GetRank() { return rank; }
    public CardSuit GetSuit() { return suit; }
    public bool IsRare() { return isRare; }

    
    int IComparable.CompareTo(object other)
    {
        CardSuit[] suits = new CardSuit[] { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
        CardData otherData = (CardData)other;
        if (CharRankToIntRank(this.rank) == CharRankToIntRank(otherData.rank))
        {
            return Array.IndexOf(suits, otherData.GetSuit()).CompareTo(Array.IndexOf(suits, suit));
        } 
        else
        {
            return CharRankToIntRank(this.rank).CompareTo(CharRankToIntRank(otherData.rank));
        }
    }

    // Converts a character rank (e.g., 4 or 9 or T or A) to an integer representing its rank.
    // J = 11...A = 14
    public static int CharRankToIntRank(char rank)
    {
        if ((int)rank < 58 && (int)rank > 49) // Checks if card is numbered 2-9.
        {
            return (int) rank - '0'; // Gets integer value of the char 2-9
        }
        switch (rank) // Special rank cases
        {
            case 'T':
                return 10;
            case 'J':
                return 11;
            case 'Q':
                return 12;
            case 'K':
                return 13;
            case 'A':
                return 14; // Note that 14 should also be able to form a straight with 2345
            default:
                break;
        }
        throw new ArgumentException("Invalid argument " + rank + " passed to CharRankToIntRank(char rank)!");
    }
}
