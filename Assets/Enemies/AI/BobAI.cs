using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAI : EnemyController
{
    [SerializeField] TurnController turnController;
    override public void Action()
    {
        if (turnController.GetTurnCounter() == 5)
        {
            GetAttack(1).UseAttack();
            return;
        }
        float actionChance = Random.Range(0f, 1f);
        if (actionChance > .90)
        {
            GetAttack(1).UseAttack();
        }
        else
        {
            GetAttack(0).UseAttack();
        }
    }

    override public void Death()
    {
        Debug.Log("Bob has died");
    }
}