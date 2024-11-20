using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    //Enums
    public enum Turn { TurnStart, PlayerTurn, EnemyTurn, TurnEnd }

    //Internal Variables
    [SerializeField] private Turn CurrentTurn;
    [SerializeField] private int TurnCount;

    public void Reset()
    {
        CurrentTurn = Turn.TurnStart;
        TurnCount = 1;
    }

    public Turn GetTurn()
    {
        return CurrentTurn;
    }

    public int GetTurnCount()
    {
        return TurnCount;
    }

    public void SetTurn(Turn turn)
    {
        CurrentTurn = turn;
    }

    public void Next()
    {
        switch (GetTurn())
        {
            case Turn.TurnStart:
                CurrentTurn = Turn.PlayerTurn;
                break;
            case Turn.PlayerTurn:
                CurrentTurn = Turn.EnemyTurn;
                break;
            case Turn.EnemyTurn:
                CurrentTurn = Turn.TurnEnd;
                break;
            case Turn.TurnEnd:
                CurrentTurn = Turn.TurnStart;
                TurnCount++;
                break;
            default:
                Debug.LogWarning("Invalid Turn type");
                break;
        }
    }
}
