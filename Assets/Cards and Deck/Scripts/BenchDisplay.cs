using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchDisplay : MonoBehaviour
{
    BenchController benchScript;

    private void Start()
    {
        benchScript = GetComponent<BenchController>();
    }

    public void SpreadCards()
    {
        int cardCount = benchScript.GetBenchSize();
    }
}
