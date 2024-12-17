using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private GameObject fadeInObject;
    public bool fadeInB = false;

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
            if (myUIGroup.alpha > 0) {
                myUIGroup.alpha -= (Time.deltaTime * 0.5f);
                if (myUIGroup.alpha <= 0) {
                    fadeInB = false;
                    fadeInObject.SetActive(false);
                    myUIGroup.alpha = 1;
                }
            }
        }
        
    }
}
