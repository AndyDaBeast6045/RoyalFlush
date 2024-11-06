using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public enum AttackType { standard, heavy, defend, buff, debuff }
    [SerializeField] private AttackType Type;
    [SerializeField] private string attackName;

    public abstract int GetAttackValue();

    public AttackType GetAttackType()
    {
        return Type;
    }

    public string GetAttackName()
    {
        return attackName;
    }
}