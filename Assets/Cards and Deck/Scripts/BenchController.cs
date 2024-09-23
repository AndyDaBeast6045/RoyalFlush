using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{
    int baseBenchSize; // Changes with passive items
    int thisCombatBenchSize; // Changes with Full House plays
    List<Transform> benchAnchors;

    List<CardController> bench = new List<CardController>();


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

    public static List<Transform> GetChildren(Transform t)
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < t.childCount; i++)
        {
            children.Add(t.GetChild(i));
        }
        return children;
    }
}
