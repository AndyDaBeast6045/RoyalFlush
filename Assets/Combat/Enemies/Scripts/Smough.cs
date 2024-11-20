using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smough : EnemyController
{
    private bool chargeAttack;

    override public void PlayerTurnStart()
    {
        float randomValue = Random.Range(0f, 1f);
        if (chargeAttack)
        {
            chargeAttack = false;
            SetAttackIndex(3);
        }
        else if (randomValue > 0.85)
        {
            SetAttackIndex(0);
            chargeAttack = true;
        }
        else if (randomValue > 0.5)
        {
            SetAttackIndex(2);
            chargeAttack = false;
        }
        else
        {
            SetAttackIndex(1);
            chargeAttack = false;
        }
        //Keep this at the end of PlayerTurnStart() always
        TelegraphAttack(GetAttack(GetAttackIndex()));
    }
}
