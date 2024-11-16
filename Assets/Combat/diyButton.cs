using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class diyButton : MonoBehaviour
{
    Scene EventsAndMap;

    void Start()
    {
        Debug.Log(SceneManager.sceneCount);
        EventsAndMap = SceneManager.GetSceneAt(0);
    }
    
    public void goToMap()
    {
        //SceneManager.LoadScene("EventsAndMap", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Combat");
        Camera.main.transform.position = new Vector3(-25f, 0f, -10f);
        //bool additive = true;
        /*
        string s = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            s, additive ? UnityEngine.SceneManagement.LoadSceneMode.Additive : 0);
        */

    }
}
