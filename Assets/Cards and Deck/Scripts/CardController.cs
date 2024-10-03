using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour, System.IComparable
{
    #region Instance Variables
    private BenchController bench;

    public enum CardSuit { Clubs, Diamonds, Hearts, Spades };

    [SerializeField] private string cardName;
    [SerializeField] private Sprite sprite;

    [SerializeField] private char rank; // T represents 10
    [SerializeField] private CardSuit suit;
    [SerializeField] public CardScript cardScript;
    [SerializeField] private bool isRare; // Marked true if rank is J, Q, K, or A

    private Button button;
    private SpriteRenderer render;

    DeckController deckScript;

    [SerializeField]
    public bool selected;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        render = GetComponent<SpriteRenderer>();
        bench = transform.parent.parent.GetComponent<BenchController>();
        deckScript = bench.GetDeckScript();
    }


    #region Getters
    public Sprite GetSprite() { return sprite; }
    public char GetRank() { return rank; }
    public CardSuit GetSuit() { return suit; }
    public bool IsRare() { return isRare; }
    #endregion Getters

    // Activates the card's effect.
    public void ActivateCard()
    {
        Debug.Log("Click registered.");
        //deckScript.DrawCard(); //TEST
        cardScript.ActivateCard(); // Testing purposes!!
    }

    public void ToggleSelect()
    {
        if (bench.GetSelectedCards().Count <= 5)
        {
            selected = !selected;
        }
        // Update display
    }

    // STATIC METHODS //
    public static int CharRankToIntRank(char rank)
    {
        if ((int)rank < 58 && (int)rank > 49) // Checks if card is numbered 2-9.
        {
            return (int)rank - '0'; // Gets integer value of the char 2-9
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

    #region C# STUFF!
    int IComparable.CompareTo(object other)
    {
        CardSuit[] suits = new CardSuit[] { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
        CardController otherData = (CardController)other;
        if (CharRankToIntRank(this.rank) == CharRankToIntRank(otherData.GetRank()))
        {
            return Array.IndexOf(suits, otherData.GetSuit()).CompareTo(Array.IndexOf(suits, suit));
        }
        else
        {
            return CharRankToIntRank(this.rank).CompareTo(CharRankToIntRank(otherData.GetRank()));
        }
    }

    public override string ToString()
    {
        return "Card is the " + rank + " of " + suit;
    }
    #endregion
}
