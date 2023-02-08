using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// works with a UI element to display the heatlh of a unit.
    /// </summary>
	[SerializeField]
    private Slider Slider;
	[SerializeField]
    private TroopScript HealthScript; //the script(troop script) contains the health
	[SerializeField]
    private Image fill;
	[SerializeField]
    private Color LowHealth = Color.red;
	[SerializeField]
    private Color HighHealth = Color.green;

    private void Awake()
    {
        //Observer pattern
		HealthScript.onDeath += checkDeath;
		HealthScript.onReceiveDamage += UpdateSlider_onHealthChange;
		HealthScript.onReceiveHealth += UpdateSlider_onHealthChange;
		Slider.value = HealthScript.health / HealthScript.maxHealth;
		fill.color = HighHealth;
    }

    private void UpdateSlider_onHealthChange(float health)
    {
        //Updates the slider when the health is changed
        Slider.value = HealthScript.health / HealthScript.maxHealth;
        //Blend between colors depending on quantity of health
        fill.color = Color.Lerp(LowHealth, HighHealth, HealthScript.health / HealthScript.maxHealth);
    }

    private void checkDeath(float health)
    {
        //Deactivate when dead
        Slider.gameObject.SetActive(false);
    }
}
