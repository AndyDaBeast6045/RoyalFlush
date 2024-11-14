using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private CardReferences cardReferences;

    public void Start()
    {
        cardReferences = GameObject.FindWithTag("CardReferences").GetComponent<CardReferences>();
        if (MainManager.Instance != null)
        {
            MainManager.Instance.playerCurrentHealth = 80;
            MainManager.Instance.playerMaxHealth = 80;
            MainManager.Instance.playerHandSize = 7;
            MainManager.Instance.chips = 0;
            MainManager.Instance.nextEncounter = 0;
            MainManager.Instance.deck = new List<CardObject>();
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Ace, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Five, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Six, CardObject.CardSuit.Spade));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Seven, CardObject.CardSuit.Spade));

            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Ace, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Five, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Six, CardObject.CardSuit.Club));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Seven, CardObject.CardSuit.Club));

            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Ace, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Five, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Six, CardObject.CardSuit.Heart));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Seven, CardObject.CardSuit.Heart));

            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Ace, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Two, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Three, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Four, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Five, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Six, CardObject.CardSuit.Diamond));
            MainManager.Instance.deck.Add(cardReferences.GetCard(CardObject.CardRank.Seven, CardObject.CardSuit.Diamond));
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
