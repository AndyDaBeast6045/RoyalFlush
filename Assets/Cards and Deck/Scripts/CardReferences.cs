using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "All Card References", menuName = "Card Reference List")]
public class CardReferences : ScriptableObject
{
    // For loot and shop population
    public Object[] numberCardPool { get; private set; }
    public Object[] rareCardPool { get; private set; }

    private void OnEnable()
    {
        numberCardPool = Resources.LoadAll("Cards and Deck/Prefabs/Complete/Numbered", typeof(GameObject));
        rareCardPool = Resources.LoadAll("Cards and Deck/Prefabs/Complete/Rare", typeof(GameObject));
    }
    // References to card effect scripts
    #region Numbered Card scripts

    [SerializeField]
    public CardScript two { get; }
    [SerializeField]
    public CardScript three { get; }
    [SerializeField]
    public CardScript four { get; }
    [SerializeField]
    public CardScript five { get; }
    [SerializeField]
    public CardScript six { get; }
    [SerializeField]
    public CardScript seven { get; }
    [SerializeField]
    public CardScript eight { get; }
    [SerializeField]
    public CardScript nine { get; }
    [SerializeField]
    public CardScript ten { get; }


    #endregion

    #region Face and Ace Cards
    [SerializeField]
    public CardScript jackOfClubs { get; }
    [SerializeField]
    public CardScript jackOfDiamonds { get; }
    [SerializeField]
    public CardScript jackOfHearts { get; }
    [SerializeField]
    public CardScript jackOfSpades { get; }

    [SerializeField]
    public CardScript queenOfClubs { get; }
    [SerializeField]
    public CardScript queenOfDiamonds { get; }
    [SerializeField]
    public CardScript queenOfHearts { get; }
    [SerializeField]
    public CardScript queenOfSpades { get; }

    [SerializeField]
    public CardScript kingOfClubs { get; }
    [SerializeField]
    public CardScript kingOfDiamonds { get; }
    [SerializeField]
    public CardScript kingOfHearts { get; }
    [SerializeField]
    public CardScript kingOfSpades { get; }

    [SerializeField]
    public CardScript aceOfClubs { get; }
    [SerializeField]
    public CardScript aceOfDiamonds { get; }
    [SerializeField]
    public CardScript aceOfHearts { get; }
    [SerializeField]
    public CardScript aceOfSpades { get; }

    #endregion
}
