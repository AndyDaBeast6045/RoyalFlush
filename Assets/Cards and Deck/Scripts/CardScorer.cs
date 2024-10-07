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
        hand.Sort();
    }



    // Applies for pairs, threes, fours, and fives
    int CheckMatching()
    {
        throw new System.Exception("not yet implemented");
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
