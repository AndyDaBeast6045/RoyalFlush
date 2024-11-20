using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void EndCombat()
    {
        Camera.main.transform.position = new Vector3(-25f, 0f, -10f);
        SceneManager.UnloadSceneAsync("Combat");
    }

    public void EndShop()
    {
        Camera.main.transform.position = new Vector3(-25f, 0f, -10f);
        SceneManager.UnloadSceneAsync("Shop");
    }
}
