//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using System.Collections;
using System.Numerics;

using Vector3 = UnityEngine.Vector3;

public class SceneChanger : MonoBehaviour
{
    public GameObject eventHead;
    public Sprite newSprite;
    private Sprite ogSprite;
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
        ogSprite = GetComponent<SpriteRenderer>().sprite;
    }

    //Method for each event type. Clicking on an event node will move the camera to the event scene.
    
    public void mapClick()
    {
        //cam.transform.position = new Vector3(0f, 0f, -10f);'
        Camera.main.transform.position = new Vector3(8.25f, 0f, -10f);
    }

    public void battleClick()
    {
        //cam.transform.position = new Vector3(27f, 0f, -10f);
        Camera.main.transform.position = new Vector3(29f, 0f, -10f);
        
        //Debug.Log("Battle Node was pressed.");
        //Camera.main.transform.position = new Vector3(47f, 0f, -10f);
    
        eventHead.GetComponent<Map>().manageNodes(transform.position.x, transform.position.y);
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

    public void changeToHoverSprite()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void changeToOGSprite()
    {
        GetComponent<SpriteRenderer>().sprite = ogSprite;
    }
}
