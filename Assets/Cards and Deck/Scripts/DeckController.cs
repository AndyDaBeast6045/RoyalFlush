using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    //[SerializeField]
    //CardReferences allGameCards;

    List<CardController> deck;
    // Start is called before the first frame update
    void Start()
    {
        deck = new List<CardController> ();
    }

    public void AddCard(CardController card)
    {
        deck.Add(card);
    }

    public void RemoveCard(CardController card)
    {
        deck.Remove(card);
    }
    
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
    }
}
