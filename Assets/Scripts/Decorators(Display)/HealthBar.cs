using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField]
    private Slider Slider;
	[SerializeField]
    private TroopScript HealthScript;
	[SerializeField]
    private Image fill;
	[SerializeField]
    private Color LowHealth = Color.red;
	[SerializeField]
    private Color HighHealth = Color.green;
    //TODO: Show only when recently damaged? Also, variables for color!

    private void Awake()
    {
		HealthScript.onDeath += checkDeath;
		HealthScript.onReceiveDamage += UpdateSlider_onHealthChange;
		HealthScript.onReceiveHealth += UpdateSlider_onHealthChange;
		Slider.value = HealthScript.health / HealthScript.maxHealth;
		fill.color = HighHealth;
    }

    private void UpdateSlider_onHealthChange(float health)
    {
        Slider.value = HealthScript.health / HealthScript.maxHealth;
        fill.color = Color.Lerp(LowHealth, HighHealth, HealthScript.health / HealthScript.maxHealth);
    }

    private void checkDeath(float health)
    {
        Slider.gameObject.SetActive(false);
    }
}
