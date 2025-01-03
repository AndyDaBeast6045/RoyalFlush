using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    //Internal Variables
    [SerializeField] private GameObject[] enemyList;
    [SerializeField] private Encounter[] encounterList;
    [SerializeField] private int encounterIndex;
    [SerializeField] private Transform[] enemyPositions;
    [SerializeField] private GameObject[] encounterEnemies;
    private bool isVictory;

    //External Variables
    private PlayerController playerControllerObject;
    private TurnController turnControllerObject;
    private CardController cardControllerObject;
    private CardReferences cardReferences;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private RewardCardObject[] rewardCards;
    [SerializeField] private TMP_Text rewardChips;
    [SerializeField] private TMP_Text currentChips;
    [SerializeField] private TMP_Text currentTurn;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private CombatMusicController combatMusicController;
    [SerializeField] private GameObject trueVictoryCanvas;


    // Start is called before the first frame update
    void Start()
    {
        isVictory = false;
        currentChips.text = MainManager.Instance.chips.ToString();
        playerControllerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
        cardControllerObject = GameObject.FindGameObjectWithTag("CardController").GetComponent<CardController>();
        cardReferences = GameObject.FindWithTag("CardReferences").GetComponent<CardReferences>();
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
        if ((enemyList.Length == 0) && (isVictory == false))
        {
            isVictory = true;
            if (MainManager.Instance.miniboss)
            {
                MinibossVictory();
            }
            else if (MainManager.Instance.finalBattle)
            {
                FinalVictory();
            }
            else
            {
                Victory();
            }
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
                playerControllerObject.Burn();
                cardControllerObject.ClearHand();
                cardControllerObject.DrawHand();
                for (int i = 0; i < enemyList.Length; i++)
                {
                    enemyList[i].GetComponent<EnemyController>().PlayerTurnStart();
                }
                endTurnButton.SetActive(true);
                break;
            case TurnController.Turn.EnemyTurn:
                enemyList[0].GetComponent<EnemyController>().EnemyTurnStart();
                break;
            case TurnController.Turn.TurnEnd:
                Next();
                break;
        }
        currentTurn.text = turnControllerObject.GetTurnCount().ToString();
    }

    public void NextEnemyTurn(GameObject currentEnemy)
    {
        if ((GetIndexOfGameObject(enemyList, currentEnemy) + 1) >= enemyList.Length)
        {
            Next();
        }
        else
        {
            enemyList[GetIndexOfGameObject(enemyList, currentEnemy) + 1].GetComponent<EnemyController>().EnemyTurnStart();
        }
    }

    public int GetIndexOfGameObject(GameObject[] objs, GameObject target)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] == target) return i;
        }
        return -1;
    }

    public void Next()
    {
        turnControllerObject.Next();
        TurnUpdate();
    }

    public void Victory()
    {
        combatMusicController.Victory();
        victoryCanvas.SetActive(true);
        int chipsGained = (int)((Random.Range(200f, 400f)) * MainManager.Instance.chipsMultiplier);
        MainManager.Instance.chips += chipsGained;
        MainManager.Instance.chipsMultiplier += 0.1;
        rewardChips.text = chipsGained + " chips";
        for (int i = 0; i < rewardCards.Length; i++)
        {
            CardObject reward = cardReferences.GetRandomCard();
            rewardCards[i].SetCard(reward);
            MainManager.Instance.deck.Add(reward);
        }
        MainManager.Instance.playerCurrentHealth = playerControllerObject.GetHealth();
        MainManager.Instance.nextEncounter = Random.Range(4,7);
    }

    public void Defeat()
    {
        combatMusicController.Defeat();
        defeatCanvas.SetActive(true);
    }


    public void MinibossVictory()
    {
        MainManager.Instance.nextEncounter = Random.Range(4, 7);
        MainManager.Instance.miniboss = false;
        SceneManager.UnloadSceneAsync("EventsAndMap");
        SceneManager.LoadScene("EventsAndMap", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Combat");
    }

    public void FinalVictory()
    {
        trueVictoryCanvas.SetActive(true);
    }
}
