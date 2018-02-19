using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpin : MonoBehaviour
{
	private float spinTime;

	private void Start()
	{
		spinTime = 1.0f;
	}

	public void Spin(int val)
	{
		StartCoroutine(SpinRoutine(val));
	}

	private IEnumerator SpinRoutine(int sign)
	{
		float startSpin = Time.time;
		float spinForce = Random.Range(4.0f, 8.0f);
		while (Time.time - startSpin < spinTime / 2)
		{
			float time = (Time.time - startSpin) / (spinTime / 2);
			transform.Rotate(Vector3.up, spinForce * time * sign);
			yield return null;
		}
		while (Time.time - startSpin < spinTime)
		{
			float time = 1 - ((Time.time - startSpin) / (spinTime / 2));
			transform.Rotate(Vector3.up, spinForce * time * sign);
			yield return null;
		}
	}
}
