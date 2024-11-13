using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private GameObject[] enemyList;
    [SerializeField] private Encounter[] encounterList;
    [SerializeField] private int encounterIndex;
    [SerializeField] private Transform[] enemyPositions;
    [SerializeField] private GameObject[] encounterEnemies;

    //External Variables
    private PlayerController playerControllerObject;
    private TurnController turnControllerObject;
    private CardController cardControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
        cardControllerObject = GameObject.FindGameObjectWithTag("CardController").GetComponent<CardController>();
        encounterIndex = MainManager.Instance.nextEncounter;
        encounterEnemies = encounterList[encounterIndex].GetEnemies();
        for (int i = 0; i < encounterEnemies.Length; i++)
        {
            Instantiate(encounterEnemies[i], enemyPositions[i]);
        }
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        turnControllerObject.Reset();
        Next();
    }

    void Update()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList.Length == 0)
        {
            Victory();
        }
    }

    public void AreaDamage(int damage)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i].GetComponent<EnemyController>().Damage(damage);
        }
    }

    public void TurnUpdate()
    {
        switch (turnControllerObject.GetTurn())
        {
            case TurnController.Turn.TurnStart:
                Next();
                break;
            case TurnController.Turn.PlayerTurn:
                playerControllerObject.ResetShield();
                cardControllerObject.ClearHand();
                cardControllerObject.DrawHand();
                for (int i = 0; i < enemyList.Length; i++)
                {
                    enemyList[i].GetComponent<EnemyController>().PlayerTurnStart();
                }
                break;
            case TurnController.Turn.EnemyTurn:
                for (int i = 0; i < enemyList.Length; i++)
                {
                    enemyList[i].GetComponent<EnemyController>().EnemyTurnStart();
                }
                Next();
                break;
            case TurnController.Turn.TurnEnd:
                Next();
                break;
        }

    }

    public void Next()
    {
        turnControllerObject.Next();
        TurnUpdate();
    }

    public void Victory()
    {
        MainManager.Instance.playerCurrentHealth = playerControllerObject.GetHealth();
        MainManager.Instance.nextEncounter = (int)Random.Range(1,3);
        SceneManager.LoadScene(3);
    }
}
