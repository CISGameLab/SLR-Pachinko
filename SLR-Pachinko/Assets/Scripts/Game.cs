using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	private int score;

	private int endScore;
	private int highScore;

	private int fireBallsLeft;

	private int actualBallsLeft;

	public TMP_Text scoreText;
	public TMP_Text ballText;

	public Button fireButton;
	public Slider slider;

	public List<GameObject> liveBalls;

	public GameObject cam;
	private float cameraLerpTime;

	private FireCooldown fireCooldown;

	public TMP_Text finalScore;
	public TMP_Text highScoreResults;
	public TMP_Text highScoreMain;
	public TMP_Text scoreEarned;

	private AudioSource[] sounds;

	private void Start()
	{
		sounds = GetComponents<AudioSource>();

		RenderSettings.ambientLight = Color.black;

		highScore = PlayerPrefs.GetInt("score");
		highScoreMain.text = "High Score: " + highScore.ToString();
		highScoreResults.text = "High Score: " + highScore.ToString();
		fireCooldown = fireButton.gameObject.GetComponent<FireCooldown>();
		cameraLerpTime = 2.5f;
		sounds[0].Play(); //intro music
		Reset();
	}

	private void Reset()
	{
		score = 0;
		fireBallsLeft = 25;
		actualBallsLeft = 25;
		liveBalls = new List<GameObject>();

		fireButton.interactable = true;
		slider.value = 1.0f;

		scoreText.text = 0. ToString();
		ballText.text = 25. ToString();
	}

	public void SetScore(int add)
	{
		score += add;
		scoreText.text = score.ToString();
	}

	public void ResetScore()
	{
		score = 0;
	}

	public void FireBall()
	{
		fireBallsLeft--;
		ballText.text = fireBallsLeft.ToString();
		slider.value = 0.0f;
		fireButton.interactable = false;
		if (fireBallsLeft > 0)
		{
			fireCooldown.StartCoolDown();
		}
	}

	public void RemoveBall()
	{
		actualBallsLeft--;
		if (actualBallsLeft <= 0)
		{
			EndGame();
			StartCoroutine(EndGameDelay());
		}
	}

	private IEnumerator EndGameDelay()
	{
		yield return new WaitForSeconds(2.0f);
		StartCoroutine(LerpCamera(90.0f));
		sounds[0].Play(); //intro music
	}

	private IEnumerator LerpCamera(float endRot, int mult = 1)
	{
		float lerpStart = Time.time;
		Quaternion rot = new Quaternion();
		rot.eulerAngles = new Vector3(cam.transform.rotation.eulerAngles.x, endRot, cam.transform.eulerAngles.z);
		while (Time.time - lerpStart < (cameraLerpTime * mult))
		{
			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, rot, ((Time.time - lerpStart) / (cameraLerpTime * mult)));
			yield return null;
		}
	}

	public void PlayPinNoise(float vel)
	{
		sounds[1].volume = 0.10f + 0.05f * vel;
		sounds[1].pitch = 1.0f + 0.05f * vel;
		sounds[1].Play();
	}

	public void PlayDiscNoise(float vel)
	{
		sounds[6].volume = 0.10f + 0.05f * vel;
		sounds[6].pitch = 1.0f + 0.05f * vel;
		sounds[6].Play();
	}

	public void PlayCupNoise()
	{
		sounds[4].Play();
	}

	public void PlayMissNoise()
	{
		sounds[5].Play();
	}

	public void PlayFireNoise()
	{
		sounds[7].Play();
	}

	private void EndGame()
	{
		endScore = (int) Mathf.Floor(score / 50);
		scoreEarned.text = "Points Earned: " + score.ToString();
		finalScore.text = "Cans Earned: " + endScore.ToString();
		SetScore();
	}

	private void SetScore()
	{
		//sounds[2].Play();//menu music
		StopAllCoroutines();
		if (PlayerPrefs.HasKey("score"))
		{
			if (endScore > PlayerPrefs.GetInt("score"))
			{
				SetHighScore();
				sounds[3].Play(); //high score
				//display high score UI
			}
		}
		else
		{
			SetHighScore();
		}
		highScore = PlayerPrefs.GetInt("score");
		highScoreMain.text = "High Score: " + highScore.ToString();
		highScoreResults.text = "High Score: " + highScore.ToString();
	}

	private void SetHighScore()
	{
		PlayerPrefs.SetInt("score", endScore);
	}

	public void ButtonNoise()
	{
		sounds[2].Play(); //button noise
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		sounds[0].Stop();
		Reset();
		StopAllCoroutines();
		StartCoroutine(LerpCamera(180.0f));
	}

	public void HowToPlay()
	{
		StopAllCoroutines();
		StartCoroutine(LerpCamera(360.0f));
	}

	public void Back()
	{
		StopAllCoroutines();
		StartCoroutine(LerpCamera(270.0f));
	}

	public void Finish()
	{
		StopAllCoroutines();
		StartCoroutine(LerpCamera(270.0f, 4));
	}
}
