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
    private TroopScript troopScript;
	[SerializeField]
    private Image fill;
    private Color LowHealth = Color.red;
    private Color HighHealth = Color.green;

    private void Awake()
    {
		troopScript.onDeath += checkDeath;
		troopScript.onReceiveDamage += UpdateSlider_onHealthChange;
		troopScript.onReceiveHealth += UpdateSlider_onHealthChange;
		Slider.value = 1f;
		fill.color = HighHealth;
    }

    private void UpdateSlider_onHealthChange(float health)
    {
		float maxHealth = troopScript.GetMaxHealth();
        Slider.value = health / maxHealth;
        fill.color = Color.Lerp(LowHealth, HighHealth, health / maxHealth);
    }

    private void checkDeath(float health)
    {
        Slider.gameObject.SetActive(false);
    }
}
