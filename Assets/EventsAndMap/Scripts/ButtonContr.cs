using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ButtonContr : MonoBehaviour
{
    public GameObject background;
    public GameObject expoText;
    public GameObject outline;
    public GameObject image;
    public GameObject resultText;
    public GameObject but1;
    public GameObject but2;
    public GameObject dropLabel;
    public GameObject exitBut;
    private int betAmount;
    private CardReferences cardReferences;

    // Start is called before the first frame update
    void Start()
    {
        cardReferences = GameObject.FindWithTag("CardReferences").GetComponent<CardReferences>();
        betAmount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeUI()
    {
        background.SetActive(false);
        expoText.SetActive(false);
        outline.SetActive(false);
        image.SetActive(false);
        but1.SetActive(false);
        but2.SetActive(false);
        dropLabel.SetActive(false);
        exitBut.SetActive(false);
        resultText.SetActive(false);
        Camera.main.transform.position = new Vector3(-25f, 0f, -10f);
    }

    public void getBetAmount()
    {
        Debug.Log(dropLabel.GetComponent<TMPro.TextMeshProUGUI>().text);
        switch (dropLabel.GetComponent<TMPro.TextMeshProUGUI>().text) {
            case "100 Chips":
                betAmount = 100;
                break;
            case "500 Chips":
                betAmount = 500;
                break;
            case "1000 Chips":
                betAmount = 1000;
                break;
        }
    }
    public void bet()
    {
        getBetAmount();
        but1.SetActive(false);
        but2.SetActive(false);
        //true for red, false for black
        int blackOrRed = Random.Range(0, 2);

        if (blackOrRed == 0)
        {
            resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Congrats! You walk away with 2x your bet!";
            MainManager.Instance.chips += betAmount * 2;
        } else {
            resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Too bad! You walk away dejectdedly, mourning your lost chips";
            MainManager.Instance.chips -= betAmount * 2;
        }
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void getSmallHeal()
    {
        MainManager.Instance.playerCurrentHealth += 50;
        if (MainManager.Instance.playerCurrentHealth > MainManager.Instance.playerMaxHealth)
        {
            MainManager.Instance.playerCurrentHealth = MainManager.Instance.playerMaxHealth;
        }
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Having made your choice, you journey onwards...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void loseACard()
    {
        MainManager.Instance.deck.RemoveAt(Random.Range(0, MainManager.Instance.deck.Count));
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Having made your choice, you journey onwards...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void loseChipsToBouncer()
    {
        MainManager.Instance.chips -= 250;
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "\"Good choice, coward!\" yells the bouncer as you sheepishly slink away...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void fightBouncer()
    {
        MainManager.Instance.nextEncounter = 1;
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
        closeUI();
    }

    public void spendChipsAtBar()
    {
        MainManager.Instance.chips -= 200;
        MainManager.Instance.playerCurrentHealth += 100;
        if (MainManager.Instance.playerCurrentHealth > MainManager.Instance.playerMaxHealth)
        {
            MainManager.Instance.playerCurrentHealth = MainManager.Instance.playerMaxHealth;
        }
        MainManager.Instance.deck.Add(cardReferences.GetRandomCard());
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Feeling stronger than ever, you venture forward into the unknown...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void runAwayFromManInSuit()
    {
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "You turn away from the mysterious figure. Some risks aren't worth taking.";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void ignoreManInSuit()
    {
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "You turn away from the mysterious figure. Some deals aren't worth taking.";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void convertHPIntoChips()
    {
        MainManager.Instance.chips += 1000;
        MainManager.Instance.playerCurrentHealth -= 200;
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "An unexpected result, but a happy one none the less...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }
}
