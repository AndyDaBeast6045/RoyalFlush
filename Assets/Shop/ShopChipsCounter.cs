using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopChipsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text currentChips;
    void Update()
    {
        currentChips.text = MainManager.Instance.chips.ToString();
    }
}
