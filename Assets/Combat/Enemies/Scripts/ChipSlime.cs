using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSlime : EnemyController
{
    override public void PlayerTurnStart()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue > 0.8)
        {
            SetAttackIndex(1);
        }
        else if (randomValue > 0.7)
        {
            SetAttackIndex(2);
        }
        else
        {
            SetAttackIndex(0);
        }
        //Keep this at the end of PlayerTurnStart() always
        TelegraphAttack(GetAttack(GetAttackIndex()));
    }
}
