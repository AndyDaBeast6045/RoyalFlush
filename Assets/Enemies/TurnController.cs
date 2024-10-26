using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public enum Turn { turnstart, playerturn, enemyturn, turnend }
    [SerializeField] private Turn CurrentTurn;
    [SerializeField] private int TurnCounter;

    void Start()
    {
        CurrentTurn = Turn.turnstart;
        TurnCounter = 0;
    }

    public Turn GetTurn()
    {
        return CurrentTurn;
    }

    public int GetTurnCounter()
    {
        return TurnCounter;
    }

    public void Next()
    {
        switch (GetTurn())
        {
            case Turn.turnstart:
                CurrentTurn = Turn.playerturn;
                break;
            case Turn.playerturn:
                CurrentTurn = Turn.enemyturn;
                break;
            case Turn.enemyturn:
                CurrentTurn = Turn.turnend;
                break;
            case Turn.turnend:
                CurrentTurn = Turn.turnstart;
                TurnCounter++;
                break;
            default:
                Debug.LogWarning("Invalid Turn type");
                break;
        }
    }
}
