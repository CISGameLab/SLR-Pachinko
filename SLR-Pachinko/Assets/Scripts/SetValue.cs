using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetValue : MonoBehaviour
{
	public Slider slider;
	public TMP_Text text;

	private void Start()
	{
		slider.value = 0.5f;
		SetTextValue();
	}

	public void SetTextValue()
	{
		text.text = slider.value.ToString("#.00");
	}
}
