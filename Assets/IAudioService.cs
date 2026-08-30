using UnityEngine;

public interface IAudioService
{
  public void PlaySFX(AudioClip clip);
  public void PlayMusic(AudioClip music);
  public void PauseMusic();
  public void ResumeMusic();
  public void StopAllMusic();
}
