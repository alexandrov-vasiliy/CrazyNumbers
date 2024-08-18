using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioButton : MonoBehaviour
{
	public bool efx;

	public Sprite musicOnSprite;

	public Sprite musicOffSprite;

	public Sprite efxOnSprite;

	public Sprite efxOffSprite;

	public Image spriteButton;

	private AudioManager _audioManager;

	[Inject]
	public void Constructor(AudioManager audioManager)
	{
		_audioManager = audioManager;
	}

	private void Start()
	{
		SetButton();
	}

	public void MusicButtonClicked()
	{
		_audioManager.MuteMusic();
		_audioManager.PlayEffects(_audioManager.buttonClick);
		SetButton();
	}

	public void EfxButtonClicked()
	{
		_audioManager.ToggleEfx();
		_audioManager.PlayEffects(_audioManager.buttonClick);
		SetButton();
	}

	private void SetButton()
	{
		if ((!_audioManager.IsMusicMute() && !efx) || (!_audioManager.IsEfxMute() && efx))
		{
			if (efx)
			{
				spriteButton.sprite = efxOnSprite;
			}
			else
			{
				spriteButton.sprite = musicOnSprite;
			}
		}
		else if (efx)
		{
			spriteButton.sprite = efxOffSprite;
		}
		else
		{
			spriteButton.sprite = musicOffSprite;
		}
	}
}
