using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyController
{
    override public void PlayerTurnStart()
    {
        if (GetTurnController().GetTurnCount() == 5)
        {
            SetAttackIndex(1);
        }
        else
        {
            float randomValue = Random.Range(0, 1);
            if (randomValue > 0.9)
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
        }
        TelegraphAttack(GetAttack(GetAttackIndex()));
    }
}
