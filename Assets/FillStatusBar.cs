using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= slider.minValue){
            fillImage.enabled = false;
        }
        if(slider.value > slider.minValue && !fillImage.enabled){
            fillImage.enabled = true;
        }

        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        if(fillValue <= slider.maxValue / 3){
            fillImage.color = Color.red;
        }
        else if(fillValue > slider.maxValue / 3){
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }

    public void inflictDamage(float damage)
    {
        if (damage > playerHealth.currentHealth)
        {
            playerHealth.currentHealth = 0;
        }
        else
        {
            playerHealth.currentHealth -= damage;
        }
    }
}
