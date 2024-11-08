using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsController : MonoBehaviour
{
    [SerializeField] private GameObject general;
    [SerializeField] private GameObject cardList;

    private void Start()
    {
        cardList.SetActive(false);
    }
    public void showGeneral()
    {
        cardList.SetActive(false);
        general.SetActive(true);
    }
    public void showCards()
    {
        general.SetActive(false);
        cardList.SetActive(true);
    }
}
