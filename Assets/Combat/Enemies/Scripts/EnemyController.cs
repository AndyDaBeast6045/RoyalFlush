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
    [SerializeField] private int burnCount;
    [SerializeField] private int attackChoiceIndex;
    [SerializeField] private int isDead;
    [SerializeField] private int nextTurnTrigger;
    [SerializeField] private bool sfxType;

    //Internal References
    [SerializeField] private GameObject telegraphObject;
    [SerializeField] private Sprite[] telegraphTextures;
    [SerializeField] private Attack[] attackPool;

    [SerializeField] private TMP_Text attackName;
    [SerializeField] private TMP_Text specialValue;
    [SerializeField] private TMP_Text attackValue;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text healthBarValue;

    [SerializeField] private GameObject shieldBar;
    [SerializeField] private TMP_Text shieldBarValue;

    [SerializeField] private GameObject burnBar;
    [SerializeField] private TMP_Text burnBarValue;

    [SerializeField] private GameObject pointerObject;
    [SerializeField] private Animator objectAnimator;

    //External Variables
    private PlayerController playerObject;
    private TurnController turnControllerObject;
    private CardController cardControllerObject;
    private CombatController combatControllerObject;
    private CombatSFXController sfxController;

    void Start()
    {
        isDead = 0;
        nextTurnTrigger = 0;
        burnCount = 0;
        enemyMaxHealth = (int)(enemyMaxHealth * Random.Range(0.8f, 1.2f));
        enemyCurrentHealth = enemyMaxHealth;
        //enemyCurrentHealth = 1;
        enemyShield = 0;
        attackChoiceIndex = 0;

        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
        cardControllerObject = GameObject.FindWithTag("CardController").GetComponent<CardController>();
        combatControllerObject = GameObject.FindWithTag("CombatController").GetComponent<CombatController>();
        sfxController = GameObject.FindWithTag("SFXController").GetComponent<CombatSFXController>();

        UpdateHealthBar();
    }
    void Update()
    {
        if (isDead == 1)
        {
            Debug.Log(enemyName + " has died.");
            Destroy(gameObject);
        }
        if (nextTurnTrigger == 1)
        {
            nextTurnTrigger = 0;
            NextEnemyTurn();
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
            if (cardControllerObject.GetClubValue() > 0)
            {
                combatControllerObject.AreaDamage(cardControllerObject.GetClubValue());
            }
            cardControllerObject.ClearSelected();
            pointerObject.GetComponent<SpriteRenderer>().enabled = false;
            sfxController.PlaySlash();
        }
    }

    public int GetHealth()
    {
        return enemyCurrentHealth;
    }
    public int GetAttackIndex()
    {
        return attackChoiceIndex;
    }
    public Attack GetAttack(int attackPoolIndex)
    {
        return attackPool[attackPoolIndex];
    }
    public TurnController GetTurnController()
    {
        return turnControllerObject;
    }

    public void SetAttackIndex(int index)
    {
        attackChoiceIndex = index;
    }

    public void Burn()
    {
        sfxController.PlayBurn();
        Damage(burnCount);
        burnCount /= 2;
        UpdateHealthBar();
    }
    public void UseAttack(Attack attack)
    {
        Attack.AttackType type = attack.GetAttackType();
        switch (type)
        {
            case Attack.AttackType.Light:
                DamagePlayer(attack.GetValue());
                break;
            case Attack.AttackType.Medium:
                DamagePlayer(attack.GetValue());
                break;
            case Attack.AttackType.Heavy:
                DamagePlayer(attack.GetValue());
                break;
            case Attack.AttackType.Shield:
                Shield(attack.GetValue());
                break;
            case Attack.AttackType.Heal:
                Heal(attack.GetValue());
                break;
            case Attack.AttackType.SpecialOffense:
                DamagePlayer(attack.GetValue(), attack.GetSpecialValue());
                break;
            case Attack.AttackType.SpecialDefense:
                DamagePlayer(attack.GetValue());
                Shield(attack.GetSpecialValue());
                break;
            case Attack.AttackType.SpecialHeal:
                DamagePlayer(attack.GetValue());
                Heal(attack.GetSpecialValue());
                break;
            default:
                break;
        }
    }

    public void DamagePlayer(int damage)
    {
        if (damage > 0)
        {
            playerObject.Damage(damage);
        }
    }
    public void DamagePlayer(int damage, int specialValue)
    {
        if (damage > 0)
        {
            playerObject.Damage(damage);
        }
    }

    public PlayerController GetPlayer()
    {
        return playerObject;
    }
    public void Damage(int damage)
    {
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
            objectAnimator.SetTrigger("DeathTrigger");
        }
        else
        {
            objectAnimator.SetTrigger("Damaged");
            UpdateHealthBar();
        }
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
        UpdateHealthBar();
    }

    public void TelegraphAttack(Attack attack)
    {
        attackName.text = attack.GetName();
        objectAnimator.SetTrigger("TeleSwitch");
        attackValue.text = null;
        specialValue.text = null;
        telegraphObject.GetComponent<SpriteRenderer>().sprite = null;
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
                attackValue.text = attack.GetValue() + " damage";
                specialValue.text = attack.GetSpecialValue() + " burn";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[5];
                break;
            case Attack.AttackType.SpecialDefense:
                attackValue.text = attack.GetValue() + " damage";
                specialValue.text = attack.GetSpecialValue() + " shield";
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[6];
                break;
            case Attack.AttackType.SpecialHeal:
                if (attack.GetValue() > 0)
                {
                    attackValue.text = attack.GetValue() + " damage";
                    specialValue.text = attack.GetSpecialValue() + " heal";
                }
                else
                {
                    attackValue.text = null;
                    specialValue.text = null;
                }
                telegraphObject.GetComponent<SpriteRenderer>().sprite = telegraphTextures[7];
                break;
            default:
                break;
        }
    }
    public abstract void PlayerTurnStart();

    public void EnemyTurnStart()
    {
        if (burnCount > 0)
        {
            Burn();
        }
        enemyShield = 0;
        if (enemyCurrentHealth <= 0)
        {
            NextEnemyTurn();
            objectAnimator.SetTrigger("DeathTrigger");
        }
        else
        {
            UseAttack(attackPool[attackChoiceIndex]);
            objectAnimator.SetTrigger("AttackTrigger");
            if (sfxType)
            {
                sfxController.PlaySlash();
            }
            else
            {
                sfxController.PlayBlunt();
            }
        }
    }

    public void NextEnemyTurn()
    {
        combatControllerObject.NextEnemyTurn(gameObject);
    }

    public void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = ((float)enemyCurrentHealth / (float)enemyMaxHealth);
        healthBarValue.text = enemyCurrentHealth + " / " + enemyMaxHealth;

        if (enemyShield > 0)
        {
            shieldBar.SetActive(true);
            shieldBar.GetComponent<Slider>().value = ((float)enemyShield / (float)enemyMaxHealth/2);
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
}
