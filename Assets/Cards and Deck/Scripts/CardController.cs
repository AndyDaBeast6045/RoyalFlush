using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour, System.IComparable
{
    private BenchController bench;

    [SerializeField]
    private CardData data;
    private Button button;
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        render = GetComponent<SpriteRenderer>();
        bench = transform.parent.GetComponent<BenchController>();
        render.sprite = data.GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateCard()
    {
        Debug.Log("Click registered.");
        data.cardScript.ActivateCard(); // Testing purposes!!
    }

    // C# STUFF!
    int IComparable.CompareTo(object other)
    {
        CardController otherCard = (CardController)other;
        return CardData.CharRankToIntRank(this.data.GetRank()).CompareTo(CardData.CharRankToIntRank(otherCard.GetCardData().GetRank()));
    }

    public CardData GetCardData()
    {
        return data;
    }

    public override string ToString()
    {
        return data.ToString();
    }
}
