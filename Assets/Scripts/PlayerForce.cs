using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerForce : MonoBehaviour
{
	public Text highScoreLabel;

	public Text currentScoreGameOverLabel;

	public Text highScoreGameOverLabel;
	
	public float highScore;

	public Action<int> OnPlayerForceUpdate;
	public int Value => _value;

	private bool counting;
	
	private int _value = 1;
	

	private void Start()
	{
		if (!PlayerPrefs.HasKey("HighScore"))
		{
			PlayerPrefs.SetFloat("HighScore", 0f);
		}
		highScore = PlayerPrefs.GetFloat("HighScore");
		UpdateHighScore();
		ResetCurrentForce();
	}

	private void UpdateHighScore()
	{
		if (Value > highScore)
		{
			highScore = Value;
		}
		highScoreLabel.text = highScore.ToString("F1");
		PlayerPrefs.SetFloat("HighScore", highScore);
	}
	

	public void ResetCurrentForce()
	{
		ResetPlayerForce();
	}

	public void IncrementPlayerForce(float number)
	{
		_value +=(int)number;
		OnPlayerForceUpdate?.Invoke(_value);
	}

	public void MultiplyPlayerForce(float number)
	{
		_value = (int)(_value * number);
		OnPlayerForceUpdate?.Invoke(_value);
	}
	
	public void DividePlayerForce(float number)
	{
		_value = (int)(_value / number);
		OnPlayerForceUpdate?.Invoke(_value);
	}

	private void ResetPlayerForce()
	{
		_value = 1;
		OnPlayerForceUpdate?.Invoke(_value);
	}
	public void UpdateScoreGameover()
	{
		UpdateHighScore();
		ResetPlayerForce();
		currentScoreGameOverLabel.text = Value.ToString("F1");
		highScoreGameOverLabel.text = highScore.ToString("F1");
	}
}
