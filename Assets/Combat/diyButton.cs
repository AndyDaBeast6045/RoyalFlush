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
        EventsAndMap = SceneManager.GetSceneAt(4);
    }
    
    public void goToMap()
    {
        SceneManager.SetActiveScene(EventsAndMap);
    }
}
