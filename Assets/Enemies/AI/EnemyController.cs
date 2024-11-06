using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private Attack[] attackPool;
    [SerializeField] private GameObject telegraph;
    [SerializeField] private Object[] telegraphTextures;

    // Start is called before the first frame update
    void Start()
    {
        telegraphTextures = Resources.LoadAll("Enemy/AttackTelegraphs",typeof(Texture2D));
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

    public void Damage(int damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(int healing)
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

    public void TelegraphAttack(int attackPoolIndex)
    {
        Attack.AttackType attackType = attackPool[attackPoolIndex].GetAttackType();
        Texture2D telegraphSprite = telegraphTextures[0];
        var spriteRenderer = telegraph.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(telegraphSprite, Rect(0,0,10,10), Vector2.zero);
    }

    public Attack GetAttack(int attackPoolIndex)
    {
        return attackPool[attackPoolIndex];
    }

    public abstract void Action();

    public abstract void Death();
}
