using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
	public Game game;

	public GameObject steelBall;
	public Slider slider;

	public void FireBall()
	{
		GameObject ball = GameObject.Instantiate(steelBall, transform.position, transform.rotation);
		float force = slider.value * 40f + 105f + Random.Range(-1f, 1f);
		ball.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
	}
}
