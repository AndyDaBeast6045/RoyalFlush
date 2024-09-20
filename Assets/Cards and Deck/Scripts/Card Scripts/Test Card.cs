using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Effects/Test Card")]
public class TestCard : CardScript
{
    public override void ActivateCard()
    {
        Debug.Log("Test card activated.");
    }
}
