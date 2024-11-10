using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Enemy/Attack", order = 0)]
public class Attack : ScriptableObject
{
    //Enums
    public enum AttackType { Light, Medium, Heavy, Shield, Heal, SpecialOffense, SpecialDefense, Special };
    public enum AttackSpecial { Normal, Burn, Regen, Vulnerable, Sturdy, Weak, Heal, Shield };

    //Variables
    [SerializeField] private AttackType type;
    [SerializeField] private AttackSpecial special;
    [SerializeField] private string attackName;
    [SerializeField] private int value;
    [SerializeField] private int specialValue;

    public string GetName()
    {
        return attackName;
    }

    //
    public AttackType GetAttackType()
    {
        return type;
    }

    public int GetValue()
    {
        return value;
    }
    //

    //
    public AttackSpecial GetSpecial()
    {
        return special;
    }

    public int GetSpecialValue() 
    {
        return specialValue;
    }
    //
}