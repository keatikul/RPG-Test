using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STMBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxSTM(float satamina)
	{
		slider.maxValue = satamina;
		slider.value = satamina;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetStm(float satamina)
	{
		slider.value = satamina;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
