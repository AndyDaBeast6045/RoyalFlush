using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField]
    CardReferences allGameCards;
    [SerializeField]
    BenchController benchScript;
    [SerializeField]
    DiscardPileController discardPile;

    [SerializeField]
    List<CardController> fullDeck; // List of all cards the player possesses
    [SerializeField]
    List<CardController> deck; // List of cards the player has yet to draw. Note that i=0 is the top of the deck.

    // Start is called before the first frame update
    void Start()
    {
        InitiatePlayDeck();
        UpdateDeck();
    }

    public void UpdateDeck()
    {
        deck.Clear();
        List<RectTransform> children = BenchController.GetChildren(GetComponent<RectTransform>());
        foreach (RectTransform r in children)
        {
            deck.Add(r.GetComponentInChildren<CardController>());
        }
    }

    // Adds every card from the fullDeck to the playing deck, then shuffles.
    // Call at the start of combat, and when reshuffling after drawing all of the cards in the deck.
    public void InitiatePlayDeck()
    {
        for (int i = 0; i < discardPile.discardPile.Count; i++)
        {
            discardPile.discardPile[i].transform.parent.SetParent(transform);
        }
        ShuffleDeck();
    }

    // Adds a card to the player's fullDeck.
    public void AddCard(CardController card)
    {
        fullDeck.Add(card);
    }

    // Removes a card from the player's fullDeck.
    public void RemoveCard(CardController card)
    {
        fullDeck.Remove(card);
    }
    
    // Shuffles the playing deck.
    public void ShuffleDeck()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int m = Random.Range(0, n + 1);
            CardController temp = deck[m];
            deck[m] = deck[n];
            deck[n] = temp;
        }
        UpdateDeck();
    }

    // Draws the topmost card to the bench, removing it from the playing deck. Refreshes deck if it's empty.
    // Not sure if reference variable stuff messes this up.
    public CardController DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("Reshuffling");
            InitiatePlayDeck();
            discardPile.ClearDiscardPile();
        }
        CardController card = benchScript.AddToBench(deck[0]);
        deck.RemoveAt(0);
        UpdateDeck();
        return card;
    }
}
