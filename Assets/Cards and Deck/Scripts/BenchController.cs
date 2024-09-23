using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{
    int baseBenchSize; // Changes with passive items
    int thisCombatBenchSize; // Changes with Full House plays
    List<RectTransform> benchAnchors;

    [SerializeField]
    int maxOffset;

    List<CardController> bench = new List<CardController>();

    new RectTransform transform;

    public void UpdateBench()
    {
        bench.Clear();
        CardController[] benchArray = transform.GetComponentsInChildren<CardController>();
        foreach (CardController card in benchArray)
        {
            bench.Add(card);
        }
        SortBench();
    }
    public void SortBench()
    {
        bench.Sort();
    }

    private void Start()
    {
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
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < t.childCount; i++)
        {
            children.Add(t.GetChild(i));
        }
        return children;
    }

    private void DisplayBench()
    {
        if (bench.Count % 2 == 0)
        {
            DisplayEvenBench();
        }
        else
        {
            DisplayOddBench();
        }
    }

    private void DisplayOddBench()
    {
        RectTransform[] cardPositions = new RectTransform[bench.Count];
        for (int i = 0; i < bench.Count; i++)
        {
            cardPositions[i] = bench[i].GetComponent<RectTransform>();
            cardPositions[i].anchoredPosition = new Vector2()
        }
    }

    private void DisplayEvenBench()
    {

    }

    private Vector2 CalculateCardPosition(int index)
    {
        Vector2 cardPos = new Vector2();
        // Lerp 
        return cardPos;
    }
}
