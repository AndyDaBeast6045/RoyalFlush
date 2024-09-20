using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To create a new CardData asset: right click in project window > Create > New Card, then fill in the data.

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject
{
    public enum CardSuit {Clubs, Diamonds, Hearts, Spades};

    [SerializeField] private string cardName;
    [SerializeField] private Sprite sprite;

    [SerializeField] private char rank;
    [SerializeField] private CardSuit suit;
    [SerializeField] public CardScript cardScript;

    public Sprite GetSprite() { return sprite; }
    public char GetRank() { return rank; }
    public CardSuit GetSuit() { return suit; }
}
