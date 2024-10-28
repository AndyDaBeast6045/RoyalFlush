using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{
    [SerializeField]
    int baseBenchSize; // Changes with passive items, default 7
    [SerializeField]
    int thisCombatBenchSize; // Changes with Full House plays

    [SerializeField]
    DeckController deckScript;
    [SerializeField]
    DiscardPileController discardScript;

    [SerializeField]
    public List<CardController> bench; // Important! This is the List representing our bench.
    private CardScorer cardScorer;

    public void UpdateBench()
    {
        bench.Clear();
        CardController[] benchArray = transform.GetComponentsInChildren<CardController>();
        foreach (CardController card in benchArray)
        {
            bench.Add(card);
        }
        SortBench();
        // DisplayBench();
    }
    void SortBench()
    {
        bench.Sort();
    }

    public int GetBenchSize() { return thisCombatBenchSize; }

    private void Awake()
    {
        bench = new List<CardController>();
        cardScorer = GetComponent<CardScorer>();
        UpdateBench();
    }

    private void Start()
    {
        thisCombatBenchSize = baseBenchSize;
        RefillBench();
    }

    public DeckController GetDeckScript() { return deckScript; }

    public CardController AddToBench(CardController card)
    {
        card.transform.parent.SetParent(transform, false);
        bench.Add(card);
        UpdateBench();
        return card;
    }

    public List<CardController> GetSelectedCards()
    {
        List<CardController> selectedCards = new List<CardController>();
        for (int i = 0; i < bench.Count; i++)
        {
            if (bench[i].selected)
            {
                selectedCards.Add(bench[i]);
            }
        }
        return selectedCards;
    }

    public void PlayHand()
    {
        List<CardController> selected = GetSelectedCards();
        cardScorer.ScoreHand();
        // call activate on all cards, scoring determined in cardcontroller
        foreach (CardController card in selected)
        {
            card.ActivateCard();
        }
        for (int i = 0; i < bench.Count; i++)
        {
            if (selected.Contains(bench[i]))
            {
                discardScript.Discard(i);
                i--;
            }
        }
        UpdateBench();
        RefillBench();
    }

    public void RefillBench()
    {
        int cardsToDraw = thisCombatBenchSize - bench.Count;
        for (int i = 0; i < cardsToDraw; i++)
        {
            deckScript.DrawCard();
        }
        UpdateBench();
    }
}
