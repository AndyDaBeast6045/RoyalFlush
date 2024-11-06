using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TurnController turnController;
    [SerializeField] EnemyController enemy;
    // Start is called before the first frame update
    void Start()
    {
        turnController.Reset();
        player.Reset();
        enemy.Reset();
        Next();
    }

    //For testing purposes
    public void Reset()
    {
        turnController.Reset();
        player.Reset();
        enemy.Reset();
        Next();
    }

    // Update is called once per frame
    public void ActionUpdate()
    {
        if (player.GetHealth() <= 0)
        {
            player.Death();
        }
        if (enemy.GetHealth() <= 0)
        {
            enemy.Death();
        }
        if (enemy.GetAlive() == false)
        {
            Debug.Log("You win.");
        }
    }

    public void TurnUpdate()
    {
        switch (turnController.GetTurn())
        {
            case TurnController.Turn.turnstart:
                Next();
                break;
            case TurnController.Turn.playerturn:
                enemy.PlayerTurnStart();
                break;
            case TurnController.Turn.enemyturn:
                enemy.EnemyTurnStart();
                ActionUpdate();
                Next();
                break;
            case TurnController.Turn.turnend:
                Next();
                break;
        }

    }

    public void Next()
    {
        turnController.Next();
        TurnUpdate();
    }
}
