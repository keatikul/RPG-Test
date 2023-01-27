using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxMP(float mana)
	{
		slider.maxValue = mana;
		slider.value = mana;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetMP(float mana)
	{
		slider.value = mana;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
