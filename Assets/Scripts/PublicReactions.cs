using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicReactions : MonoBehaviour
{
  [SerializeField] List<AudioSource> audioSources;
  [SerializeField] List<AudioClip> positiveReactionSoundEffects;
  [SerializeField] List<AudioClip> negativeReactionSoundEffects;
  [SerializeField] List<AudioClip> begginningSoundEffects;
  [SerializeField] float maxDelay = 0.2f;
  [SerializeField] int minSoundAmmount = 3;
  [SerializeField] int maxSoundAmmount = 5;
  public void BegginingSounds()
  {

  }

  public void PositiveReaction()
  {
    PlayRandomSFXFromRandomPositions(positiveReactionSoundEffects, Random.Range(minSoundAmmount, maxSoundAmmount));
  }
  public void NegativeReaction()
  {
    PlayRandomSFXFromRandomPositions(negativeReactionSoundEffects, Random.Range(minSoundAmmount, maxSoundAmmount));
  }


  private void PlayRandomSFXFromRandomPositions(List<AudioClip> sfx, int clipsToPlay)
  {
    List<AudioClip> chosenClips = Utilities.PickRandomFromList(sfx, Mathf.Min(audioSources.Count, clipsToPlay));
    List<AudioSource> chosenSources = Utilities.PickRandomFromList(audioSources, clipsToPlay);
    for (int i = 0; i < clipsToPlay; i++)
    {
      StartCoroutine(PlayDelayedOneshot(chosenSources[i], chosenClips[i], Random.Range(0, maxDelay)));
    }
  }

  private IEnumerator PlayDelayedOneshot(AudioSource source, AudioClip clip, float delay)
  {
    yield return new WaitForSeconds(delay);
    source.PlayOneShot(clip);
  }
}
