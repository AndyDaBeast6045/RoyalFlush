using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttack", menuName = "EnemyLogic/DamageAttack", order = 1)]
public class DamageAttack : Attack
{
    [SerializeField] private int damage;

    override public void UseAttack()
    {
        Debug.Log(GetAttackName() + " has been used, dealing " + damage + " damage.");
    }
}