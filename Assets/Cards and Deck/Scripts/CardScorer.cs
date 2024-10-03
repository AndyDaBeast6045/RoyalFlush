using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScorer : MonoBehaviour
{
    List<CardController> hand;
    [SerializeField]
    BenchController bench;

    #region Yo this is such a dumb solution
    int clubAttackCount;
    int clubDefenseCount;
    int diamondAttackCount;
    int diamondDefenseCount;
    int heartAttackCount;
    int heartDefenseCount;
    int spadeAttackCount;
    int spadeDefenseCount;
    #endregion

    void UpdateHand()
    {
        hand = bench.GetSelectedCards();
    }


}
