using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
	public Game game;

	private Rigidbody rb;
	private float force;
	private Vector3 pos;
	private float tLastPos;

	private int score_cup;
	private int score_pin;
	private int score_disc;

	private AudioSource sound;

	// Use this for initialization
	private void Start()
	{
		sound = GetComponent<AudioSource>();

		game = GameObject.Find("GameState").GetComponent<Game>();
		game.liveBalls.Add(gameObject);

		rb = GetComponent<Rigidbody>();
		force = 5.0f;
		tLastPos = Time.time;

		score_cup = 50;
		score_pin = 1;
		score_disc = 10;
	}

	private void FixedUpdate()
	{
		if (Time.time - tLastPos > 0.5f)
		{
			if (Vector3.Distance(pos, transform.position) < 0.1f)
			{
				rb.AddForce(GetForce(), ForceMode.Impulse);
			}
			pos = transform.position;
			tLastPos = Time.time;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "pin")
		{
			game.SetScore(score_pin);
			rb.AddForce(GetForce(), ForceMode.Impulse);
			game.PlayPinNoise(rb.velocity.magnitude);
		}
		else if (other.gameObject.tag == "disc")
		{
			game.SetScore(score_disc);
			if (other.gameObject.transform.position.x < transform.position.x)
			{
				other.gameObject.GetComponent<DiscSpin>().Spin(-1);
			}
			else
			{
				other.gameObject.GetComponent<DiscSpin>().Spin(1);
			}
			game.PlayDiscNoise(rb.velocity.magnitude);
		}
		else if (other.gameObject.tag == "cup")
		{
			game.SetScore(score_cup);
			game.PlayCupNoise();
		}
		else if (other.gameObject.tag == "miss")
		{
			game.PlayMissNoise();
		}
		else if (other.gameObject.tag == "spiral")
		{
			sound.Play();
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "spiral")
		{
			sound.Stop();
		}
	}

	private Vector3 GetForce()
	{
		return new Vector3(Random.Range(-force, force), 0f, 0f);
	}

	private void OnDestroy()
	{
		game.liveBalls.Remove(gameObject);
		game.RemoveBall();
	}
}
