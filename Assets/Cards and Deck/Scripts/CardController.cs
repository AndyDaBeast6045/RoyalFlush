using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardData data;
    private Button button;
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        render = GetComponent<SpriteRenderer>();
        render.sprite = data.GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateCard()
    {
        Debug.Log("Click registered.");
        data.cardScript.ActivateCard(); // Testing purposes!!
    }
}
