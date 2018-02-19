using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCooldown : MonoBehaviour
{
	public Slider slider;
	public Button button;
	private float cooldownTime;

	// Use this for initialization
	private void Start()
	{
		cooldownTime = 1.0f;
		slider.value = 1.0f;
	}

	// Update is called once per frame
	public void StartCoolDown()
	{
		StartCoroutine(CoolDown());
	}

	private IEnumerator CoolDown()
	{
		float coolDownStart = Time.time;
		float time = 0.0f;
		while (time < 1.0f)
		{
			time = (Time.time - coolDownStart) / cooldownTime;
			slider.value = time;
			yield return null;
		}
		button.interactable = true;
	}
}
