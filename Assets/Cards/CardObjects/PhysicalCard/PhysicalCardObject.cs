using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalCardObject : MonoBehaviour
{
    [SerializeField] private bool isSelected;
    [SerializeField] private CardObject assignedCard;
    [SerializeField] private Image selectedObject;

    void Start()
    {
        isSelected = false;
        selectedObject.enabled = false;
    }

    public void SelectToggle()
    {
        if (isSelected)
        {
            isSelected = false;
            selectedObject.enabled = false;
        }
        else
        {
            isSelected = true;
            selectedObject.enabled = true;
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetCard(CardObject card)
    {
        assignedCard = card;
    }

    public CardObject GetCard()
    {
        return assignedCard;
    }

    public CardObject.CardRank GetRank()
    {
        return assignedCard.GetRank();
    }

    public CardObject.CardSuit GetSuit()
    {
        return assignedCard.GetSuit();
    }

    public void SetSprite()
    {
        GetComponent<Image>().sprite = assignedCard.GetSprite();
    }
}
