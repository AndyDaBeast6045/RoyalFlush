using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    //Enums
    public enum CardSet { Invalid, High, Pair, TwoPair, Three, Straight, Flush, Full, Four, StraightFlush, RoyalFlush };

    //Internal Variables
    [SerializeField] private List<CardObject> deck;
    [SerializeField] private List<CardObject> discard;
    [SerializeField] private List<GameObject> hand;
    [SerializeField] private List<GameObject> selectedCards;
    [SerializeField] private int drawHandSize;
    [SerializeField] private int handValue;
    [SerializeField] private CardSet handSet;
    [SerializeField] private int clubValue;
    [SerializeField] private int heartValue;
    [SerializeField] private int spadeValue;
    [SerializeField] private int diamondValue;

    //External Variables
    [SerializeField] private TMP_Text setDisplayObject;
    [SerializeField] private TMP_Text setValueObject;
    [SerializeField] private Transform handObject;
    [SerializeField] private GameObject blankCard;
    [SerializeField] private CombatSFXController sfxController;
    [SerializeField] private TMP_Text deckDisplay;
    [SerializeField] private TMP_Text discardDisplay;

    // Start is called before the first frame update
    void Start()
    {
        sfxController = GameObject.FindWithTag("SFXController").GetComponent<CombatSFXController>();
        deck = new List<CardObject>();
        discard = new List<CardObject>();
        hand = new List<GameObject>();
        handSet = CardSet.Invalid;
        selectedCards = new List<GameObject>();
        handValue = 0;
        if (MainManager.Instance != null)
        {
            for (int i = 0; i < MainManager.Instance.deck.Count; i++)
            {
                deck.Add(MainManager.Instance.deck[i]);
            }
            drawHandSize = MainManager.Instance.playerHandSize;
        }
        Shuffle();
        UpdateDisplay();
        UpdateCardDisplay();
        DrawHand();
    }

    public void UpdateCardDisplay()
    {
        deckDisplay.text = deck.Count.ToString();
        discardDisplay.text = discard.Count.ToString();
    }

    public void DrawHand()
    {
        DrawAmount(drawHandSize);
    }

    public int GetHandValue()
    {
        return handValue;
    }

    public int GetClubValue()
    {
        return clubValue;
    }

    public int GetHeartValue()
    {
        return heartValue;
    }

    public int GetSpadeValue()
    {
        return spadeValue;
    }

    public int GetDiamondValue()
    {
        return diamondValue;
    }

    public void Shuffle()
    {
        List<CardObject> shuffled = new List<CardObject>();
        while (deck.Count > 0)
        {
            int index = Random.Range(0, deck.Count - 1);
            shuffled.Add(deck[index]);
            deck.RemoveAt(index);
        }
        deck = shuffled;
    }

    public void UpdateDisplay()
    {
        switch (handSet)
        {
            case CardSet.Invalid:
                setDisplayObject.text = "Invalid Set";
                break;
            case CardSet.High:
                setDisplayObject.text = "High Card";
                break;
            case CardSet.TwoPair:
                setDisplayObject.text = "Two Pair";
                break;
            case CardSet.Three:
                setDisplayObject.text = "Three Of A Kind";
                break;
            case CardSet.Full:
                setDisplayObject.text = "Full House";
                break;
            case CardSet.Four:
                setDisplayObject.text = "Four Of A Kind";
                break;
            case CardSet.StraightFlush:
                setDisplayObject.text = "Straight Flush";
                break;
            case CardSet.RoyalFlush:
                setDisplayObject.text = "Royal Flush";
                break;
            default:
                setDisplayObject.text = handSet.ToString();
                break;
        }
        setValueObject.text = handValue + " Damage";
        if (clubValue > 0)
        {
            setValueObject.text += " | " + clubValue + " AOE Damage";
        }
        if (heartValue > 0)
        {
            setValueObject.text += " | " + heartValue + " Burn Inflicted";
        }
        if (spadeValue > 0)
        {
            setValueObject.text +=  " | " + spadeValue + " Cards Drawn";
        }
        if (diamondValue > 0)
        {
            setValueObject.text += " | " + diamondValue + " Shield Gained";
        }
    }

    public void Draw()
    {
        hand.Add(CreateCard(deck[0]));
        deck.RemoveAt(0);
        if (deck.Count == 0)
        {
            Recycle();
        }
        UpdateCardDisplay();
    }

    public void DrawAmount(int cardsDrawn)
    {
        for (int i = 0; i < cardsDrawn; i++)
        {
            Draw();
        }
    }

    public void ClearHand()
    {
        selectedCards.Clear();
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            discard.Add(hand[i].GetComponent<PhysicalCardObject>().GetCard());
            Destroy(hand[i]);
            hand.RemoveAt(i);
        }
        EvaluateSetValue();
        UpdateDisplay();
        UpdateCardDisplay();
    }

    public void ClearSelected()
    {
        for (int i = selectedCards.Count - 1; i >= 0; i--)
        {
            discard.Add(selectedCards[i].GetComponent<PhysicalCardObject>().GetCard());
            hand.Remove(selectedCards[i]);
            Destroy(selectedCards[i]);
            selectedCards.RemoveAt(i);
        }
        selectedCards.Clear();
        EvaluateSetValue();
        UpdateDisplay();
        UpdateCardDisplay();
    }

    public void Recycle()
    {
        for (int i = discard.Count - 1; i >= 0; i--)
        {
            deck.Add(discard[i]);
            discard.RemoveAt(i);
        }
        Shuffle();
        UpdateCardDisplay();
    }

    public void SelectCard(GameObject selectedCard)
    {
        if (selectedCards.Contains(selectedCard))
        {
            selectedCard.GetComponent<PhysicalCardObject>().SelectToggle();
            selectedCards.Remove(selectedCard);
        }
        else
        {
            selectedCard.GetComponent<PhysicalCardObject>().SelectToggle();
            selectedCards.Add(selectedCard);
        }
        CheckSet();
        UpdateDisplay();
        UpdateCardDisplay();
    }

    public CardSet GetCurrentSet()
    {
        return handSet;
    }

    public void EvaluateSetValue()
    {
        handValue = 0;
        clubValue = 0;
        heartValue = 0;
        spadeValue = 0;
        diamondValue = 0;
        if (handSet != CardSet.Invalid)
        {
            for (int i = 0; i < selectedCards.Count; i++)
            {
                switch (selectedCards[i].GetComponent<PhysicalCardObject>().GetRank())
                {
                    case CardObject.CardRank.Ace:
                        handValue += 1;
                        break;
                    case CardObject.CardRank.Two:
                        handValue += 2;
                        break;
                    case CardObject.CardRank.Three:
                        handValue += 3;
                        break;
                    case CardObject.CardRank.Four:
                        handValue += 4;
                        break;
                    case CardObject.CardRank.Five:
                        handValue += 5;
                        break;
                    case CardObject.CardRank.Six:
                        handValue += 6;
                        break;
                    case CardObject.CardRank.Seven:
                        handValue += 7;
                        break;
                    case CardObject.CardRank.Eight:
                        handValue += 8;
                        break;
                    case CardObject.CardRank.Nine:
                        handValue += 9;
                        break;
                    case CardObject.CardRank.Ten:
                        handValue += 10;
                        break;
                    case CardObject.CardRank.Jack:
                        handValue += 11;
                        break;
                    case CardObject.CardRank.Queen:
                        handValue += 12;
                        break;
                    case CardObject.CardRank.King:
                        handValue += 13;
                        break;
                    default:
                        break;
                }
                if ((int)handSet != (int)CardSet.High)
                {
                    switch (selectedCards[i].GetComponent<PhysicalCardObject>().GetSuit())
                    {
                        case CardObject.CardSuit.Club:
                            clubValue += 1;
                            break;
                        case CardObject.CardSuit.Heart:
                            heartValue += 1;
                            break;
                        case CardObject.CardSuit.Spade:
                            spadeValue += 1;
                            break;
                        case CardObject.CardSuit.Diamond:
                            diamondValue += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (handSet)
            {
                case CardSet.Pair:
                    handValue = (int)((double)handValue * 1.5);
                    break;
                case CardSet.TwoPair:
                    handValue = (int)((double)handValue * 2);
                    break;
                case CardSet.Three:
                    handValue = (int)((double)handValue * 2.5);
                    break;
                case CardSet.Straight:
                    handValue = (int)((double)handValue * 3);
                    break;
                case CardSet.Flush:
                    handValue = (int)((double)handValue * 3.5);
                    break;
                case CardSet.Full:
                    handValue = (int)((double)handValue * 4);
                    break;
                case CardSet.Four:
                    handValue = (int)((double)handValue * 4.5);
                    break;
                case CardSet.StraightFlush:
                    handValue = (int)((double)handValue * 6);
                    break;
                case CardSet.RoyalFlush:
                    handValue = (int)((double)handValue * 7);
                    break;
                default:
                    break;
            }
            heartValue = (int)(heartValue * handValue * 0.20);
            clubValue = (int)(clubValue * handValue * 0.15);
            diamondValue = (int)(diamondValue * handValue * 0.30);
        }
    }

    public GameObject CreateCard(CardObject card)
    {
        GameObject newCard = Instantiate(blankCard, handObject);
        newCard.GetComponent<PhysicalCardObject>().SetCard(card);
        newCard.name = card.GetName();
        newCard.GetComponent<PhysicalCardObject>().SetSprite();
        return newCard;
    }

    public void CheckSet()
    {
        switch (selectedCards.Count)
        {
            case 1:
                handSet = CardSet.High;
                break;
            case 2:
                if ((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank())
                {
                    handSet = CardSet.Pair;
                }
                else
                {
                    handSet = CardSet.Invalid;
                }
                break;
            case 3:
                if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank())
                    && ((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank()))
                {
                    handSet = CardSet.Three;
                }
                else
                {
                    handSet = CardSet.Invalid;
                }
                break;
            case 4:
                if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank())
                    && ((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank()))
                    && ((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank()))
                {
                    handSet = CardSet.Four;
                }
                else if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank())
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() == (int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank()))
                {
                    handSet = CardSet.TwoPair;
                }
                else
                {
                    handSet = CardSet.Invalid;
                }
                break;
            case 5:
                if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetRank() - 1)))
                {
                    handSet = CardSet.StraightFlush;
                }
                else if ((((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank()))
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank()))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetRank()))) 
                    || (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank()))
                    && ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank()))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetRank()))))
                {
                    handSet = CardSet.Full;
                }
                else if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetSuit()))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetSuit() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetSuit())))
                {
                    handSet = CardSet.Flush;
                }
                else if (((int)selectedCards[0].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[1].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[2].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() - 1))
                    && ((int)selectedCards[3].GetComponent<PhysicalCardObject>().GetRank() == ((int)selectedCards[4].GetComponent<PhysicalCardObject>().GetRank() - 1)))
                {
                    handSet = CardSet.Straight;
                }
                else
                {
                    handSet = CardSet.Invalid;
                }
                break;
            default:
                handSet = CardSet.Invalid;
                break;
        }
        EvaluateSetValue();
    }
}
