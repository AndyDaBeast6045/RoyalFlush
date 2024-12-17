using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private GameObject fadeInObject;
    private bool fadeInB = false;

    public void fadeIn() 
    {
        myUIGroup.alpha = 0;
        fadeInObject.SetActive(true);
        fadeInB = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (fadeInB) {
            if (myUIGroup.alpha < 1) {
                myUIGroup.alpha += Time.deltaTime;
                if (myUIGroup.alpha >= 1) {
                    fadeInB = false;
                }
            }
        }
        
    }
}
