//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using System.Collections;
using System.Numerics;

using Vector3 = UnityEngine.Vector3;

public class SceneChanger : MonoBehaviour
{
    
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

    //Method for each event type. Clicking on an event node will move the camera to the event scene.
    
    public void mapClick()
    {
        //cam.transform.position = new Vector3(0f, 0f, -10f);'
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
    }

    public void battleClick()
    {
        //cam.transform.position = new Vector3(27f, 0f, -10f);
        Camera.main.transform.position = new Vector3(19f, 0f, -10f);
    }

    public void rouletteClick()
    {
        Camera.main.transform.position = new Vector3(38f, 0f, -10f);
    }

    public void manInSuitClick()
    {
        Camera.main.transform.position = new Vector3(57f, 0f, -10f);
    }

    public void snackBarClick()
    {
        Camera.main.transform.position = new Vector3(76f, 0f, -10f);
    }

    public void bathroomClick()
    {
        Camera.main.transform.position = new Vector3(95f, 0f, -10f);
    }

    public void bouncerClick()
    {
        Camera.main.transform.position = new Vector3(114f, 0f, -10f);
    }

    public void bossClick()
    {
        Camera.main.transform.position = new Vector3(133f, 0f, -10f);
    }
}
