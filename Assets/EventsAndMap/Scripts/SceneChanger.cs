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
    public Sprite newColor;
    private Color ogColor;

    //private AsyncOperation asyncLoad;
    /*
     * Specific event x coords:
     * Map: 0
     * Battle: 19
     * Roulette: 38
     * Man in Suit: 57
     * Snack Bar: 76
     * Bathroom: 95
     * Bouncer: 114
     * Boss: 133
     */

    void Start()
    {
        ogColor = GetComponent<SpriteRenderer>().color;
    }

    //Method for each event type. Clicking on an event node will move the camera to the event scene.
    
    public void mapClick()
    {
        //cam.transform.position = new Vector3(0f, 0f, -10f);'
        Camera.main.transform.position = new Vector3(8.25f, 0f, -10f);
    }

    public void shopClick()
    {
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    public void battleClick()
    {
        //cam.transform.position = new Vector3(27f, 0f, -10f);
        //Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        
        //Debug.Log("Battle Node was pressed.");
        //Camera.main.transform.position = new Vector3(47f, 0f, -10f);
    
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);

        //asyncLoad = SceneManager.LoadSceneAsync("Combat");
        //asyncLoad.allowSceneActivation = true;

        /*
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        */



        //enemySpawner.GetComponent<EnemySpawner>().spawnEnemies(enemySpawner.GetComponent<EnemySpawner>().difficultyLevel);
    }

    public void rouletteClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void manInSuitClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void snackBarClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void bathroomClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void bouncerClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
    }

    public void bossClick()
    {
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
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
}
