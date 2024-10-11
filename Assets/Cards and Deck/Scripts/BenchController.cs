using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{
    int baseBenchSize; // Changes with passive items
    int thisCombatBenchSize; // Changes with Full House plays
    List<RectTransform> benchAnchors;

    [SerializeField]
    int maxOffsetX;
    [SerializeField]
    int maxOffsetY;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    DeckController deckScript;
    [SerializeField]
    DiscardPileController discardScript;

    [SerializeField]
    public List<CardController> bench;

    new RectTransform transform;

    public void UpdateBench()
    {
        bench.Clear();
        CardController[] benchArray = transform.GetComponentsInChildren<CardController>();
        benchAnchors = GetChildren(transform);
        foreach (CardController card in benchArray)
        {
            bench.Add(card);
        }
        SortBench();
        DisplayBench();
    }
    public void SortBench()
    {
        bench.Sort();
    }

    private void Awake()
    {
        bench = new List<CardController>();
        transform = GetComponent<RectTransform>();
        benchAnchors = GetChildren(transform);
        UpdateBench();
        Debug.Log(this);
    }

    public override string ToString()
    {
        string output = string.Empty;
        foreach (CardController card in bench)
        {
            output += card.ToString() + "\n";
        }
        return output;
    }
    
    public static List<RectTransform> GetChildren(RectTransform t)
    {
        List<RectTransform> children = new List<RectTransform>();
        for (int i = 0; i < t.childCount; i++)
        {
            children.Add(t.GetChild(i).GetComponent<RectTransform>());
        }
        return children;
    }

    public DeckController GetDeckScript() { return deckScript; }

    private void DisplayBench()
    {
        for (int i = 0; i < bench.Count; i++)
        {
            benchAnchors[i].anchoredPosition3D = CalculateCardPosition(i);
            benchAnchors[i].rotation = Quaternion.Euler(0, 0, CalculateCardRotation(i));
        }
    }

    private Vector3 CalculateCardPosition(int index)
    {
        Vector3 cardPos = new Vector3();
        int halfIndex = Mathf.CeilToInt(bench.Count / 2f) - 1;
        cardPos.x = Mathf.Lerp(-maxOffsetX, maxOffsetX, index / (bench.Count - 1f));

        if (index <= halfIndex)
        {
            cardPos.y = Mathf.Lerp(-maxOffsetY, maxOffsetY, index / (float)(halfIndex));
        } else if (bench.Count % 2 == 0)
        {
            // For even bench sizes, have the same y pos for two cards in the middle
            cardPos.y = Mathf.Lerp(maxOffsetY, -maxOffsetY, (index - halfIndex - 1) / (float)(halfIndex));
        } else
        {
            cardPos.y = Mathf.Lerp(maxOffsetY, -maxOffsetY, (index - halfIndex) / (float)(halfIndex));
        }
        cardPos.z = 1 - (0.1f * index);
        return cardPos;
    }

    private float CalculateCardRotation(int index)
    {
        float zRotation = Mathf.Lerp(maxRotation, -maxRotation, index / (bench.Count - 1f));
        return zRotation;
    }

    public CardController AddToBench(CardController card)
    {
        card.transform.SetParent(transform);
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
            }
        }
        // discard all selected
    }
}
