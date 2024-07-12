using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("Audio Sources")]
	public AudioSource efxSource;

	public AudioSource musicSource;
	public AudioSource bossSource;

	[Header("Background Music")]
	public AudioClip menuMusic;

	public AudioClip gameMusic;
	
	public AudioClip gameOverMusic;

	[Header("Sound Effects")]
	public AudioClip buttonClick;

	public AudioClip gameOver;

	public AudioClip sameColor;

	public AudioClip wrongColor;
	
	public AudioClip[] bossSpawn;

	private bool muteMusic;

	private bool muteEfx;
	
	
	private void Start()
	{
		muteMusic = ((PlayerPrefs.GetInt("MuteMusic") == 1) ? true : false);
		muteEfx = ((PlayerPrefs.GetInt("MuteEfx") == 1) ? true : false);
		PlayMusic(menuMusic);
	}

	public void PlayMusic(AudioClip clip)
	{
		if (!muteMusic)
		{
			musicSource.clip = clip;
			if (!musicSource.isPlaying)
			{
				musicSource.Play();
			}
		}
	}

	private void StopMusic()
	{
		musicSource.Stop();
	}

	public void PlayEffects(AudioClip clip)
	{
		if (!muteEfx)
		{
			Debug.Log($"Play Effect {clip.name}");
			efxSource.PlayOneShot(clip);
		}
	}

	public void MuteMusic()
	{
		if (muteMusic)
		{
			muteMusic = false;
			PlayMusic(menuMusic);
			PlayerPrefs.SetInt("MuteMusic", 0);
		}
		else
		{
			muteMusic = true;
			StopMusic();
			PlayerPrefs.SetInt("MuteMusic", 1);
		}
	}

	public void MuteEfx()
	{
		PlayerPrefs.SetInt("MuteEfx", muteEfx ? 0 : 1);
		muteEfx = !muteEfx;
	}

	public bool IsMusicMute()
	{
		return muteMusic;
	}

	public bool IsEfxMute()
	{
		return muteEfx;
	}

	public void PlayBossSound()
	{
		if (bossSpawn.Length > 0)
		{
			var clip = bossSpawn[Random.Range(0, bossSpawn.Length)];
			bossSource.clip = clip;
			bossSource.Play();
		}

	}
}
