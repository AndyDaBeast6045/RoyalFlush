using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardScorer : MonoBehaviour
{
    List<CardController> hand;
    [SerializeField]
    BenchController bench;

    void UpdateHand()
    {
        hand = bench.GetSelectedCards();
        hand.Sort();
    }

    public void ScoreHand()
    {
        UpdateHand();
        if (CheckRoyalFlush())
        {
            Debug.Log("Royal flush played.");
            ScoreAllCards(hand);
            // call effect (additional to straight flush)
        }
        if (CheckStraightFlush())
        {
            ScoreAllCards(hand);
            // call effect (additional to straight/flush)
        }
        if (CheckStraight())
        {
            Debug.Log("Straight played.");
            ScoreAllCards(hand);
            // call effect
        }
        if (CheckFlush())
        {
            Debug.Log("Flush played.");
            ScoreAllCards(hand);
            // call effect
        }
        if (CheckFullHouse())
        {
            Debug.Log("Full House played.");
            ScoreAllCards(hand);
            // call effect
        }
        if (CheckFourOrFive())
        {
            Debug.Log("Four/Five-of-a-kind played.");
            // call effect
        }
    }

    void ScoreAllCards(List<CardController> cards)
    {
        foreach (CardController card in cards)
        {
            card.scored = true;
        }
    }

    bool CheckStraightFlush()
    {
        return CheckFlush() && CheckStraight();
    }

    bool CheckRoyalFlush()
    {
        return CheckStraightFlush() && hand[0].GetRank() == 'T' && hand[4].GetRank() == 'A';
    }

    bool CheckFourOrFive()
    {
        if (hand.Count < 4) return false;
        int rankCount = 1;
        char rankChecking = hand[0].GetRank();
        for (int i = 1; i < hand.Count; i++)
        {
            if (hand[i].GetRank() == rankChecking)
            {
                rankCount++;
            }
        }
        if (rankCount >= 4)
        {
            return true;
        }
        else if (rankCount == 1)
        {
            rankCount = 1;
            rankChecking = hand[hand.Count - 1].GetRank();
            for (int i = 1; i < hand.Count; i++)
            {
                if (hand[i].GetRank() == rankChecking)
                {
                    rankCount++;
                }
            }
            if (rankCount >= 4)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckFullHouse()
    {
        if (hand.Count != 5)
        {
            return false;
        }
        int rankDifference = 0;
        int i = 1;
        // Find the difference between the two ranks. 
        while (rankDifference == 0 && i < hand.Count)
        {
            int temp = (int) Mathf.Abs(CardController.CompareRank(hand[0], hand[i]));
            if (rankDifference != 0 && rankDifference != temp)
            {
                return false; // If there is more than one difference, it is not a full house.
            } else // rankDifference is either 0 or temp
            {
                rankDifference = temp;
            }
            i++;
        }
        return rankDifference != 0; // If there is no difference, all cards are the same rank and it is not a full house.
    }

    bool CheckStraight() 
    {
        if (hand.Count != 5)
        {
            return false;
        }
        // Special case: A2345. Compare does not work because A is coded as 14.
        if (hand[hand.Count - 1].GetRank() == 'A' && hand[0].GetRank() == '2')
        {
            for (int i = 1; i < hand.Count - 1; i++)
            { // Check cards i=0 to 3
              // In a straight, cards are consecutively one rank higher than the next. If they are not, it is not a straight.
                if (CardController.CompareRank(hand[0], hand[i]) != i) { return false; }
            }
            return true;
        }


        for (int i = 1; i < hand.Count; i++)
        {
            // In a straight, cards are consecutively one rank higher than the next. If they are not, it is not a straight.
            if (CardController.CompareRank(hand[0], hand[i]) != i) { return false; }
        }
        return true;
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
        Debug.Log("Resonance not implemented yet");
    }
    void DiamondResonance(int atk, int def)
    {
        Debug.Log("Resonance not implemented yet");
    }
    void HeartResonance(int atk, int def)
    {
        Debug.Log("Resonance not implemented yet");
    }
    void SpadeResonance(int atk, int def)
    {
        Debug.Log("Resonance not implemented yet");
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
