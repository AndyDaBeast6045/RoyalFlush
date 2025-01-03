//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using System.Collections;
using System.Numerics;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class SceneChanger : MonoBehaviour
{
    public GameObject eventHead;

    public GameObject background;
    public GameObject expoText;
    public GameObject outline;
    public GameObject image;
    public GameObject but1;
    public GameObject but2;
    public GameObject drop;
    public GameObject exitBut;
    public GameObject resultText;

    public Sprite toilet;
    public Sprite secondSprite;

    //public Sprite newColor;
    private Color ogColor;

    void Start()
    {
        ogColor = GetComponent<SpriteRenderer>().color;
    }

    //Method for each event type. Clicking on an event node will move the camera to the event scene.
    
    public void mapClick()
    {
        Camera.main.transform.position = new Vector3(8.25f, 0f, -10f);
    }

    public void battleClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        //Debug.Log("Battle Node was pressed.");
    
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
    }

    public void enableUI()
    {
        background.SetActive(true);
        expoText.SetActive(true);
        outline.SetActive(true);
        image.SetActive(true);
        but1.SetActive(true);
        but2.SetActive(true);
    }

    public void shopClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    public void rouletteClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        enableUI();
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "The door slams behind you as you enter a room empty save for a lonely roulette table. \nIt seems there's no escape until you place a bet!";
        drop.SetActive(true);
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Bet on Red";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Bet On Black";
        image.GetComponent<Image>().sprite = toilet;
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().bet(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().bet(); });

        Camera.main.transform.position = new Vector3(-25f, -15f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void manInSuitClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        enableUI();
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "As you walk past a dark hallway, a mysterious voice calls out to you: \n\"Psst! Knight! I've heard about your journey and I', just DYING to lend you a helping hand. Follow me and I might just CUT you a deal...\"";
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Follow";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Ignore";
        image.GetComponent<Image>().sprite = toilet;
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { manInSuit2(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().runAwayFromManInSuit(); });

        Camera.main.transform.position = new Vector3(-25f, -15f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void manInSuit2()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "\"Give up 200 health and I'll reward you with 1000 chips. \nNot too shabby an offer, huh!\"";
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Accept";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Ignore";
        image.GetComponent<Image>().sprite = secondSprite;
        if (MainManager.Instance.playerCurrentHealth < 200)
        {
            but1.SetActive(false);
        }
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().convertHPIntoChips(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().ignoreManInSuit(); });
    }

    public void snackBarClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        enableUI();
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "At your wits ends, you see the familiar neon glow of the casino's snack bars. \nTake a seat, or spend your hard-earned earnings for a medium heal and a random card.";
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Rest (Heal 50)";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Spend 200 Chips (Heal 100)";
        image.GetComponent<Image>().sprite = toilet;
        if (MainManager.Instance.chips <= 200)
        {
            but2.SetActive(false);
        }
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().getSmallHeal(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().spendChipsAtBar(); });

        Camera.main.transform.position = new Vector3(-25f, -15f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void bathroomClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        enableUI();
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "Turning the corner, the welcome sight of a porcelain throne greets your eyes. You may rest here, or relieve yourself of a great burden.";
        //GameObject but1Text = GameObject.Find("Canvas/RouletteBut1/Text (TMP)");
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Lose 1 Card";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Relax (Heal 50)";
        image.GetComponent<Image>().sprite = toilet;
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().loseACard(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().getSmallHeal(); });

        Camera.main.transform.position = new Vector3(-25f, -15f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void bouncerClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        enableUI();
        expoText.GetComponent<TMPro.TextMeshProUGUI>().text = "As you venture on, a large man you recognize you recognize as a bouncer blocks your path. In a dark, burly voice he bellows:\n\"Pay the fee or pay the price!\"";
        GameObject.Find("Canvas/RouletteBut1/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Pay 250 Chips";
        GameObject.Find("Canvas/RouletteBut2/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "Fight!";
        Debug.Log("uh bounce");
        image.GetComponent<Image>().sprite = toilet;
        but1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but1.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().loseChipsToBouncer(); });
        but2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        but2.GetComponentInChildren<Button>().onClick.AddListener(delegate { but1.GetComponent<ButtonContr>().fightBouncer(); });

        Camera.main.transform.position = new Vector3(-25f, -15f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void miniBossClick()
    {
        StartCoroutine(changeToClikedSpriteAfterTime(1));
        //Replace '5' with hypothetical miniboss encounter
        MainManager.Instance.miniboss = true;
        MainManager.Instance.nextEncounter = 2;
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
    }

    public void finalBossClick()
    {
        MainManager.Instance.finalBattle = true;
        MainManager.Instance.nextEncounter = 3;
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
    }

    //Aesthetic map methods
    public void changeToHoverSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
    }

    public void changeToOGSprite()
    {
        GetComponent<SpriteRenderer>().color = ogColor;
    }

    IEnumerator changeToClikedSpriteAfterTime(float seconds) {
        yield return new WaitForSeconds(seconds);
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
}
