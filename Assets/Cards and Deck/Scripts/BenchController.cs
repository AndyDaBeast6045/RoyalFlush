using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour
{

    List<CardController> bench = new List<CardController>();

    public void SortHand()
    {
        bench.Sort();
    }

    private void Start()
    {
        CardController[] benchArray = transform.GetComponentsInChildren<CardController>();
        foreach (CardController card in benchArray)
            bench.Add(card);
        SortHand();
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
}
