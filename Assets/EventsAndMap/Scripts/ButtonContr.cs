using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ButtonContr : MonoBehaviour
{
    public GameObject mainManager;

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

    // Start is called before the first frame update
    void Start()
    {
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
            case "10 Chips":
                betAmount = 10;
                break;
            case "20 Chips":
                betAmount = 20;
                break;
            case "30 Chips":
                betAmount = 30;
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
            mainManager.GetComponent<MainManager>().changeChips(betAmount);
        } else {
            resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Too bad! You walk away dejectdedly, mourning your lost chips";
            mainManager.GetComponent<MainManager>().changeChips((betAmount * -1));
        }
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void getSmallHeal()
    {
        mainManager.GetComponent<MainManager>().changeHealth(15);
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Having made your choice, you journey onwards...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void loseACard()
    {
        //IMPLEMENT
        Debug.Log("i am master programmer");
        mainManager.GetComponent<MainManager>().loseACardFromDeck();
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Having made your choice, you journey onwards...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void loseChipsToBouncer()
    {
        mainManager.GetComponent<MainManager>().changeChips(-50);
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "\"Good choice, coward!\" yells the bouncer as you sheepishly slink away...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }

    public void fightBouncer()
    {
        //Replace '7' with harder than average battle
        mainManager.GetComponent<MainManager>().changeEncounter(7);
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
        //Above average level fight
        closeUI();
    }

    public void spendChipsAtBar()
    {
        mainManager.GetComponent<MainManager>().changeChips(-50);
        mainManager.GetComponent<MainManager>().changeHealth(30);
        //ADD GET A RANDOM CARD LIKE BATHROOM BUT OPPOSITE
        mainManager.GetComponent<MainManager>().gainACard();
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "Feeling stronger than ever, you venture forward into the unknown...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }
    /*
    public void convertCards()
    {
        //Convert 5 random cards into suite
        mainManager.GetComponent<MainManager>().convertHPIntoChips();
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "An unexpected result, but a happy one none the less...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }
    */

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
        mainManager.GetComponent<MainManager>().changeChips(30);
        mainManager.GetComponent<MainManager>().changeHealth(-10);
        resultText.GetComponent<TMPro.TextMeshProUGUI>().text = "An unexpected result, but a happy one none the less...";
        resultText.SetActive(true);
        exitBut.SetActive(true);
    }
}
