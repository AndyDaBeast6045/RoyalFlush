using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class EnemyController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private int enemyShield;
    [SerializeField] private GameObject telegraphObject;
    [SerializeField] private int attackChoiceIndex;
    [SerializeField] private int isDead;
    [SerializeField] private int burnCount;

    //Internal References
    [SerializeField] private Sprite[] telegraphTextures;
    [SerializeField] private TMP_Text attackName;
    [SerializeField] private TMP_Text specialValue;
    [SerializeField] private TMP_Text attackValue;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text healthBarValue;
    [SerializeField] private GameObject shieldBar;
    [SerializeField] private TMP_Text shieldBarValue;
    [SerializeField] private GameObject burnBar;
    [SerializeField] private TMP_Text burnBarValue;
    [SerializeField] private Attack[] attackPool;
    [SerializeField] private GameObject pointerObject;

    //External Variables
    [SerializeField] private PlayerController playerObject;
    [SerializeField] private TurnController turnControllerObject;
    [SerializeField] private CardController cardControllerObject;
    [SerializeField] private CombatController combatControllerObject;
    [SerializeField] private Animator objectAnimator;

    //
    // Start is called before the first frame update
    void Start()
    {
        isDead = 0;
        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
        cardControllerObject = GameObject.FindWithTag("CardController").GetComponent<CardController>();
        combatControllerObject = GameObject.FindWithTag("CombatController").GetComponent<CombatController>();
        burnCount = 0;
        telegraphObject.GetComponent<SpriteRenderer>().sprite = null;
        enemyMaxHealth = (int)(enemyMaxHealth * Random.Range(0.8f, 1.2f));
        enemyCurrentHealth = enemyMaxHealth;
        enemyShield = 0;
        UpdateHealthBar();
    }
    void Update()
    {
        if (isDead == 1)
        {
            Debug.Log(enemyName + " has died.");
            Destroy(gameObject);
        }
    }

    void OnMouseEnter()
    {
        if (cardControllerObject.GetCurrentSet() != CardController.CardSet.Invalid)
        {
            pointerObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnMouseExit()
    {
        pointerObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseDown()
    {
        if (cardControllerObject.GetCurrentSet() != CardController.CardSet.Invalid)
        {
            if (cardControllerObject.GetClubValue() > 0)
            {
                combatControllerObject.AreaDamage(cardControllerObject.GetClubValue());
            }
            if (cardControllerObject.GetHeartValue() > 0)
            {
                burnCount += cardControllerObject.GetHeartValue();
            }
            if (cardControllerObject.GetSpadeValue() > 0)
            {
                cardControllerObject.DrawAmount(cardControllerObject.GetSpadeValue());
            }
            if (cardControllerObject.GetDiamondValue() > 0)
            {
                playerObject.Shield(cardControllerObject.GetDiamondValue());
                playerObject.UpdateHealthBar();
            }
            Damage(cardControllerObject.GetHandValue());
            cardControllerObject.ClearSelected();
        }
    }

    public int GetHealth()
    {
        return enemyCurrentHealth;
    }

    public void Burn()
    {
        Damage(burnCount);
        burnCount /= 2;
        UpdateHealthBar();
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
        objectAnimator.SetTrigger("Damaged");
        if (enemyShield > 0)
        {
            int damageReduced = enemyShield;
            enemyShield -= damage;
            damage -= damageReduced;
            if (damage < 0)
            {
                damage = 0;
            }
            if (enemyShield < 0)
            {
                enemyShield = 0;
            }
        }
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Death();
        }
        UpdateHealthBar();
    }

    public void Heal(int healing)
    {
        enemyCurrentHealth += healing;
        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
        UpdateHealthBar();
    }

    public void Shield(int shield)
    {
        enemyShield += shield;
    }
    //

    public void TelegraphAttack(Attack attack)
    {
        attackName.text = attack.GetName();
        specialValue.text = null;
        objectAnimator.SetTrigger("TeleSwitch");
        switch (attack.GetAttackType())
        {
            case Attack.AttackType.Light:
                attackValue.text = attack.GetValue() + " damage";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[0];
                break;
            case Attack.AttackType.Medium:
                attackValue.text = attack.GetValue() + " damage";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[1];
                break;
            case Attack.AttackType.Heavy:
                attackValue.text = attack.GetValue() + " damage";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[2];
                break;
            case Attack.AttackType.Shield:
                attackValue.text = attack.GetValue() + " shield";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[3];
                break;
            case Attack.AttackType.Heal:
                attackValue.text = attack.GetValue() + " heal";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[4];
                break;
            case Attack.AttackType.SpecialOffense:
                specialValue.text = attack.GetSpecialValue() + " " + attack.GetSpecial().ToString();
                attackValue.text = attack.GetValue() + " damage";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[5];
                break;
            case Attack.AttackType.SpecialDefense:
                specialValue.text = attack.GetSpecialValue() + " " + attack.GetSpecial().ToString();
                attackValue.text = attack.GetValue() + " shield";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[6];
                break;
            case Attack.AttackType.Special:
                attackValue.text = null;
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[7];
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
        if (burnCount > 0)
        {
            Burn();
        }
        enemyShield = 0;
        UseAttack(attackPool[attackChoiceIndex]);
    }
    //

    public void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = ((float)enemyCurrentHealth / (float)enemyMaxHealth);
        healthBarValue.text = enemyCurrentHealth + " / " + enemyMaxHealth;

        if (enemyShield > 0)
        {
            shieldBar.SetActive(true);
            shieldBar.GetComponent<Slider>().value = ((float)enemyShield / (float)enemyMaxHealth);
            shieldBarValue.text = enemyShield.ToString();
        }
        else
        {
            shieldBar.SetActive(false);
        }
        if (burnCount > 0)
        {
            burnBar.SetActive(true);
            burnBarValue.text = burnCount.ToString();
        }
        else
        {
            burnBar.SetActive(false);
        }
    }

    public void Death()
    {
        objectAnimator.SetTrigger("DeathTrigger");
    }
}
