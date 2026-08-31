using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  [SerializeField] AudioSource sfxSource;
  [SerializeField] AudioSource musicSource;
  [SerializeField] AudioMixer mixer;
  public void PlayMusic(AudioClip music)
  {
    musicSource.clip = music;
    musicSource.loop = true;
    musicSource.Play();
  }

  public void PlaySFX(AudioClip clip)
  {
    sfxSource.PlayOneShot(clip);
  }

  public void StopAllMusic()
  {
    musicSource.Stop();
    musicSource.clip = null;
  }

  public void PauseMusic()
  {
    musicSource.Pause();
  }
  public void ResumeMusic()
  {
    musicSource.UnPause();
  }

  public void SetMasterVolume(float value)
  {
    SetVolume("MasterVolume", value);
  }

  public void SetMusicVolume(float value)
  {
    SetVolume("MusicVolume", value);
  }

  public void SetSFXVolume(float value)
  {
    SetVolume("SFXVolume", value);
  }

  private void SetVolume(string parameter, float value)
  {
    float volume = value <= 0.0001f
        ? -80f
        : Mathf.Log10(value) * 20f;

    mixer.SetFloat(parameter, volume);
  }
}
