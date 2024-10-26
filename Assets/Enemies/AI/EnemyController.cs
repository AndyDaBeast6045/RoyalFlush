using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private Attack[] attackPool;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public string GetName()
    {
        return enemyName;
    }

    public int GetHealth()
    {
        return enemyCurrentHealth;
    }

    public void DamageEnemy(int damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Death();
        }
    }

    public void HealEnemy(int healing)
    {
        enemyCurrentHealth += healing;
        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
    }

    public void Reset()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    public Attack GetAttack(int attackPoolIndex)
    {
        return attackPool[attackPoolIndex];
    }

    public abstract void Action();

    public abstract void Death();
}
