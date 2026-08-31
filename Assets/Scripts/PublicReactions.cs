using System.Collections.Generic;
using UnityEngine;

public class PublicReactions : MonoBehaviour
{
  [SerializeField] List<AudioSource> audioSources;
  [SerializeField] List<AudioClip> positiveReactionSoundEffects;
  [SerializeField] List<AudioClip> negativeReactionSoundEffects;
  [SerializeField] List<AudioClip> begginningSoundEffects;
  [SerializeField] float maxDelay = 0.2f;

  public void BegginingSounds()
  {

  }
  public void PositiveReaction()
  {
    PlayRandomSFXFromRandomPositions(positiveReactionSoundEffects, Random.Range(1, 4));
  }
  public void NegativeReaction()
  {
    PlayRandomSFXFromRandomPositions(negativeReactionSoundEffects, Random.Range(1, 4));
  }

  private void PlayRandomSFXFromRandomPositions(List<AudioClip> sfx, int clipsToPlay)
  {
    List<AudioClip> chosenClips = Utilities.PickRandomFromList(sfx, Mathf.Min(audioSources.Count, clipsToPlay));
    List<AudioSource> chosenSources = Utilities.PickRandomFromList(audioSources, clipsToPlay);
    for (int i = 0; i < clipsToPlay; i++)
    {
      chosenSources[i].clip = chosenClips[i];
      chosenSources[i].PlayDelayed(Random.Range(0, maxDelay));
    }
  }
}
