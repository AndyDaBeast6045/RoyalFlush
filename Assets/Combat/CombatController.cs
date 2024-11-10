using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    //External Variables
    [SerializeField] PlayerController playerObject;
    [SerializeField] TurnController turnControllerObject;
    [SerializeField] GameObject[] enemyList;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        turnControllerObject = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        Next();
    }

    // Update is called once per frame
    public void ActionUpdate()
    {
        if (playerObject.GetHealth() <= 0)
        {
            playerObject.Death();
        }
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].GetComponent<EnemyController>().GetHealth() <= 0)
            {
                enemyList[i].GetComponent<EnemyController>().Death();
            }
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
                for (int i = 0; i < enemyList.Length; i++)
                {
                    enemyList[i].GetComponent<EnemyController>().PlayerTurnStart();
                }
                break;
            case TurnController.Turn.EnemyTurn:
                for (int i = 0; i < enemyList.Length; i++)
                {
                    enemyList[i].GetComponent<EnemyController>().EnemyTurnStart();
                    ActionUpdate();
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
}
