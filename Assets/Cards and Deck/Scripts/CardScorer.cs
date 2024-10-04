using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScorer : MonoBehaviour
{
    List<CardController> hand;
    [SerializeField]
    BenchController bench;

    void UpdateHand()
    {
        hand = bench.GetSelectedCards();
    }

    // Applies for pairs, threes, fours, and fives
    bool CheckMatching()
    {
        throw new System.Exception("not yet implemented");
    }

    bool CheckFullHouse()
    {
        throw new System.Exception("not yet implemented");
    }

    bool CheckStraight() 
    {
        throw new System.Exception("not yet implemented");
    }

    bool CheckFlush()
    {
        if (hand.Count != 5) { return false; }
        bool flush = true;

        CardController.CardSuit suit = hand[0].GetSuit();
        foreach (CardController card in hand)
        {
            if (card.GetSuit() != suit)
            {
                flush = false; break;
            }
        }
        return flush;
    }

    #region Resonance Effects
    void ClubResonance(int atk, int def) 
    { 
        
    }
    void DiamondResonance(int atk, int def)
    {

    }
    void HeartResonance(int atk, int def)
    {

    }
    void SpadeResonance(int atk, int def)
    {

    }
    #endregion

    // you do NOT wanna see this method
    void CallResonance()
    {
        #region please don't look
        int clubAttackCount = 0;
        int clubDefenseCount = 0;
        int diamondAttackCount = 0;
        int diamondDefenseCount = 0;
        int heartAttackCount = 0;
        int heartDefenseCount = 0;
        int spadeAttackCount = 0;
        int spadeDefenseCount = 0;
        #endregion
        foreach (CardController card in hand)
        {
            switch (card.GetSuit())
            {
                case CardController.CardSuit.Clubs:
                    switch (card.GetCardType())
                    {
                        case CardController.CardType.Attack:
                            clubAttackCount++; break;
                        case CardController.CardType.Defense:
                            clubDefenseCount++; break;
                        case CardController.CardType.Hybrid:
                            clubAttackCount++; clubDefenseCount++; break;
                    }
                    break;
                case CardController.CardSuit.Diamonds:
                    switch (card.GetCardType())
                    {
                        case CardController.CardType.Attack:
                            diamondAttackCount++; break;
                        case CardController.CardType.Defense:
                            diamondDefenseCount++; break;
                        case CardController.CardType.Hybrid:
                            diamondAttackCount++; diamondDefenseCount++; break;
                    }
                    break;
                case CardController.CardSuit.Hearts:
                    switch (card.GetCardType())
                    {
                        case CardController.CardType.Attack:
                            heartAttackCount++; break;
                        case CardController.CardType.Defense:
                            heartDefenseCount++; break;
                        case CardController.CardType.Hybrid:
                            heartAttackCount++; heartDefenseCount++; break;
                    }
                    break;
                case CardController.CardSuit.Spades:
                    switch (card.GetCardType())
                    {
                        case CardController.CardType.Attack:
                            spadeAttackCount++; break;
                        case CardController.CardType.Defense:
                            spadeDefenseCount++; break;
                        case CardController.CardType.Hybrid:
                            spadeAttackCount++; spadeDefenseCount++; break;
                    }
                    break;
            }
        }
        ClubResonance(clubAttackCount, clubDefenseCount);
        DiamondResonance(diamondAttackCount, diamondDefenseCount);
        HeartResonance(heartAttackCount, heartDefenseCount);
        SpadeResonance(spadeAttackCount, spadeDefenseCount);
    }


    
}
