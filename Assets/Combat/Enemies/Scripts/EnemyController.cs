using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private int enemyShield;
    [SerializeField] private GameObject telegraphObject;
    [SerializeField] private Sprite[] telegraphTextures;
    [SerializeField] private Attack[] attackPool;
    [SerializeField] private int attackChoiceIndex;

    //External Variables
    private PlayerController playerObject;
    private TurnController turnControllerObject;

    //
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
        telegraphObject.GetComponent<SpriteRenderer>().sprite = null;
        enemyCurrentHealth = enemyMaxHealth;
        enemyShield = 0;
    }
    //

    public string GetName()
    {
        return enemyName;
    }
    
    public int GetMaxHealth()
    {
        return enemyMaxHealth;
    }

    public int GetHealth()
    {
        return enemyCurrentHealth;
    }

    public int GetShield()
    {
        return enemyShield;
    }

    //
    public int GetAttackIndex()
    {
        return attackChoiceIndex;
    }

    public void SetAttackIndex(int index)
    {
        attackChoiceIndex = index;
    }
    //

    public TurnController GetTurnController()
    {
        return turnControllerObject;
    }

    //
    public Attack GetAttack(int attackPoolIndex)
    {
        return attackPool[attackPoolIndex];
    }

    public void UseAttack(Attack attack)
    {
        Attack.AttackType type = attack.GetAttackType();
        switch (type)
        {
            case Attack.AttackType.Light:
                DamagePlayer(attack.GetValue(), attack.GetSpecial(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.Medium:
                DamagePlayer(attack.GetValue(), attack.GetSpecial(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.Heavy:
                DamagePlayer(attack.GetValue(), attack.GetSpecial(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.Shield:
                Shield(attack.GetValue());
                if (attack.GetSpecial() != Attack.AttackSpecial.Normal)
                {
                    BuffSelf(attack.GetSpecial(), attack.GetSpecialValue());
                }
                break;
            case Attack.AttackType.Heal:
                Heal(attack.GetValue());
                if (attack.GetSpecial() != Attack.AttackSpecial.Normal)
                {
                    BuffSelf(attack.GetSpecial(), attack.GetSpecialValue());
                }
                break;
            case Attack.AttackType.SpecialOffense:
                DamagePlayer(0, attack.GetSpecial(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.SpecialDefense:
                DamagePlayer(attack.GetValue(), attack.GetSpecial(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.Special:
                DamagePlayer(attack.GetValue(), attack.GetSpecial(), attack.GetSpecialValue());
                break;
            default:
                break;
        }
    }

    public void DamagePlayer(int damage, Attack.AttackSpecial specialType, int specialValue)
    {
        switch (specialType)
        {
            case Attack.AttackSpecial.Burn:
                break;
            case Attack.AttackSpecial.Vulnerable:
                break;
            case Attack.AttackSpecial.Weak:
                break;
            default:
                break;
        }
        playerObject.Damage(damage);
    }

    public void BuffSelf(Attack.AttackSpecial specialType, int specialValue)
    {
        Debug.Log("Buff");
    }
    //

    public PlayerController GetPlayer()
    {
        return playerObject;
    }

    //
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

    public void Shield(int shield)
    {
        enemyShield += shield;
    }
    //

    public void TelegraphAttack(Attack attack)
    {
        switch (attack.GetAttackType())
        {
            case Attack.AttackType.Light:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[0];
                break;
            case Attack.AttackType.Medium:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[1];
                break;
            case Attack.AttackType.Heavy:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[2];
                break;
            case Attack.AttackType.Shield:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[3];
                break;
            case Attack.AttackType.Heal:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[4];
                break;
            case Attack.AttackType.SpecialOffense:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[5];
                break;
            case Attack.AttackType.SpecialDefense:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[6];
                break;
            case Attack.AttackType.Special:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[8];
                break;
            default:
                telegraphObject.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }

    //
    public abstract void PlayerTurnStart();

    public void EnemyTurnStart()
    {
        enemyShield = 0;
        UseAttack(attackPool[attackChoiceIndex]);
    }
    //

    public void Death()
    {
        Debug.Log(enemyName + " has died.");
        Destroy(gameObject);
    }
}
