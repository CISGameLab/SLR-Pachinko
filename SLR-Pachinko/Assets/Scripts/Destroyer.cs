using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "ball")
		{
			Destroy(other.gameObject);
		}
	}
}
