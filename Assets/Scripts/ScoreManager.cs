using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public Text currentScoreLabel;

	public Text highScoreLabel;

	public Text currentScoreGameOverLabel;

	public Text highScoreGameOverLabel;

	public float currentScore;

	public float highScore;

	public Action<int> OnPlayerForceUpdate;
	public int PlayerForce => _playerForce;

	private bool counting;

	
	private int _playerForce = 1;

	public static ScoreManager Instance
	{
		get;
		set;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		if (!PlayerPrefs.HasKey("HighScore"))
		{
			PlayerPrefs.SetFloat("HighScore", 0f);
		}
		highScore = PlayerPrefs.GetFloat("HighScore");
		UpdateHighScore();
		ResetCurrentScore();
	}

	private void UpdateHighScore()
	{
		if (currentScore > highScore)
		{
			highScore = currentScore;
		}
		highScoreLabel.text = highScore.ToString("F1");
		PlayerPrefs.SetFloat("HighScore", highScore);
	}

	public void UpdateScore(int value)
	{
		currentScore += value;
		Round(currentScore, 1);
		currentScoreLabel.text = currentScore.ToString("F1");
	}

	public void ResetCurrentScore()
	{
		currentScore = 0f;
		UpdateScore(0);
		ResetPlayerForce();
	}

	public void IncrementPlayerForce(int number)
	{
		_playerForce += number;
		OnPlayerForceUpdate?.Invoke(_playerForce);
	}

	private void ResetPlayerForce()
	{
		_playerForce = 1;
		OnPlayerForceUpdate?.Invoke(_playerForce);
	}
	public void UpdateScoreGameover()
	{
		UpdateHighScore();
		ResetPlayerForce();
		currentScoreGameOverLabel.text = currentScore.ToString("F1");
		highScoreGameOverLabel.text = highScore.ToString("F1");
	}

	public void StartCounting()
	{
		counting = true;
		StartCoroutine(Counter());
	}

	public void StopCounting()
	{
		counting = false;
		StopCoroutine(Counter());
	}

	private IEnumerator Counter()
	{
		while (counting)
		{
			currentScore += 0.1f;
			Round(currentScore, 1);
			currentScoreLabel.text = currentScore.ToString("F1");
			yield return new WaitForSeconds(0.1f);
		}
	}

	public float Round(float value, int digits)
	{
		float num = Mathf.Pow(10f, digits);
		return Mathf.Round(value * num) / num;
	}
}
