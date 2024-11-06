using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private Attack[] attackPool;
    [SerializeField] private GameObject telegraphObject;
    [SerializeField] private Sprite[] telegraphTextures;
    [SerializeField] private PlayerController playerObject;
    [SerializeField] private TurnController turnController;
    [SerializeField] private int AttackChoiceIndex;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private bool alive;

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

    public int GetAttackIndex()
    {
        return AttackChoiceIndex;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAttackIndex(int index)
    {
        AttackChoiceIndex = index;
    }

    public TurnController GetTurnController()
    {
        return turnController;
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
        GetComponent<SpriteRenderer>().sprite = enemySprite;
        alive = true;
        enemyCurrentHealth = enemyMaxHealth;
    }

    public void TelegraphAttack(Attack attack)
    {
        var spriteRenderer = telegraphObject.GetComponent<SpriteRenderer>();
        Attack.AttackType type = attack.GetAttackType();
        switch (type)
        {
            case Attack.AttackType.standard:
                spriteRenderer.sprite = telegraphTextures[0];
                break;
            case Attack.AttackType.heavy:
                spriteRenderer.sprite = telegraphTextures[1];
                break;
            default:
                break;
        }
    }

    public Attack GetAttack(int attackPoolIndex)
    {
        return attackPool[attackPoolIndex];
    }

    public void UseAttack(Attack attack)
    {
        Attack.AttackType type = attack.GetAttackType();
        switch (type)
        {
            case Attack.AttackType.standard:
                playerObject.Damage(attack.GetAttackValue());
                break;
            case Attack.AttackType.heavy:
                playerObject.Damage(attack.GetAttackValue());
                break;
            default:
                break;
        }
    }

    public PlayerController GetPlayer()
    {
        return playerObject;
    }

    public abstract void PlayerTurnStart();
    public void EnemyTurnStart()
    {
        UseAttack(attackPool[AttackChoiceIndex]);
    }

    public void Death()
    {
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        alive = false;
        Debug.Log(enemyName + " has died.");
    }
}
