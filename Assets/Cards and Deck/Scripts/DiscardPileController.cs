using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPileController : MonoBehaviour
{
    [SerializeField]
    BenchController benchScript;

    [SerializeField]
    List<CardController> discardPile;

    // Start is called before the first frame update
    void Start()
    {
        discardPile = new List<CardController>();
    }

    public void AddCard(CardController card)
    {
        discardPile.Add(card);
    }

    public void ClearDiscardPile()
    {
        discardPile.Clear();
    }

    public void Discard(int index)
    {
        CardController card = benchScript.bench[index];
        discardPile.Add(card);
        benchScript.bench.RemoveAt(index);
    }
}
