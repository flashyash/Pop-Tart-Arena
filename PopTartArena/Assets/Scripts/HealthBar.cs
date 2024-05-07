using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	private int theMaxHealth = 100;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
		theMaxHealth = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		Debug.Log("I am trying to change the health bar to " + health);
		slider.value = health/10;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
