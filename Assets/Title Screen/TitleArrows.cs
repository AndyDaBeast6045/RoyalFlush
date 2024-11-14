using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject image2;

    public void Start()
    {
        image.SetActive(false);
        image2.SetActive(false);
    }
}
