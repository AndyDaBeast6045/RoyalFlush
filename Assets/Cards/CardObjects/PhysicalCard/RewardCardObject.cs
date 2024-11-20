using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCardObject : MonoBehaviour
{
    public void SetCard(CardObject card)
    {
        GetComponent<Image>().sprite = card.GetSprite();
    }
}
