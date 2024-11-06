using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAI : EnemyController
{
    public override void PlayerTurnStart()
    {
        if (GetTurnController().GetTurnCounter() == 5)
        {
            SetAttackIndex(1);
        }
        else
        {
            float actionChance = Random.Range(0f, 1f);
            if (actionChance > .90)
            {
                SetAttackIndex(1);
            }
            else
            {
                SetAttackIndex(0);
            }
        }
        TelegraphAttack(GetAttack(GetAttackIndex()));
    }
}