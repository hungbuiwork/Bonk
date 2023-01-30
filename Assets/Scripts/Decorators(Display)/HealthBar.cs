using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Health HealthScript;
    public Image fill;
    public Color LowHealth = Color.red;
    public Color HighHealth = Color.green;
    //TODO: Show only when recently damaged? Also, variables for color!

    private void Start()
    {
        if (Slider == null) Slider = this.GetComponentInChildren<Slider>();
        if (HealthScript == null) HealthScript = this.GetComponentInParent<Health>();
        if (HealthScript != null && Slider != null)
        {
            HealthScript.onReceiveDamage += UpdateSlider_onHealthChange;
            HealthScript.onReceiveHealth += UpdateSlider_onHealthChange;
            Slider.value = HealthScript.CurrHealth / HealthScript.MaxHealth;
            fill.color = HighHealth;
            HealthScript.onDeath += checkDeath;
        }
    }

    private void UpdateSlider_onHealthChange(float currentHealth)
    {
        if (Slider != null && HealthScript != null)
        {
            Slider.value = HealthScript.CurrHealth / HealthScript.MaxHealth;
            fill.color = Color.Lerp(LowHealth, HighHealth, HealthScript.CurrHealth / HealthScript.MaxHealth);
        }
    }

    private void checkDeath(float check)
    {
        Debug.Log("Dead");
    }
}
