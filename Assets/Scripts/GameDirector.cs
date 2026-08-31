using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
  [SerializeField] List<LevelData> levels;
  [SerializeField] GameObject truchanContainer;
  [SerializeField] ButtonDestroyer buttonDestroyer;
  [SerializeField] WaveSpawner waveSpawner;
  [SerializeField] AudioManager audioManager;
  [SerializeField] float waitBeforeFirstWave = 3f;
  [SerializeField] AudioClip winMusic;
  [SerializeField] AudioClip loseMusic;
  bool levelFinished = false;
  bool playerDied = false;

  private void Start()
  {
    StartCoroutine(ExecuteLevel(levels[0]));
  }
  IEnumerator ExecuteLevel(LevelData level)
  {
    audioManager.PlayMusic(level.music);
    if (truchanContainer.transform.childCount > 0)
      Destroy(truchanContainer.transform.GetChild(0).gameObject);
    var truchanPrefab = Instantiate(level.truchanPrefab, truchanContainer.transform);
    var truchanAnimator = truchanPrefab.GetComponent<Animator>();
    waveSpawner.OnWaveFinished.AddListener(() => truchanAnimator.SetTrigger("reset"));
    waveSpawner.OnWaveCollectionFinished.AddListener(OnLevelFinished);

    yield return new WaitForSeconds(waitBeforeFirstWave);
    StartCoroutine(waveSpawner.SpawnWaveCollection(level.waves));
    yield return new WaitUntil(() => levelFinished || playerDied);
    if (levelFinished)
    {
      truchanAnimator.SetTrigger("win");
      audioManager.PlayMusic(winMusic);
      yield return new WaitForSeconds(winMusic.length);
      if (level.nextLevel == null)
      {
        Debug.Break(); // TODO: IR A CINEMATICA FINAL
      }
      else
      {
        // TODO: TRANSICIONAR A PROXIMO NIVEL
        StartCoroutine(ExecuteLevel(level.nextLevel));
      }
    }
    else if (playerDied)
    {
      truchanAnimator.SetTrigger("lose");
      buttonDestroyer.enabled = false;
      waveSpawner.StopAllCoroutines();
      waveSpawner.enabled = false;
      foreach (var button in Utilities.GetAllButtons())
      {
        button.OnPressed();
      }
      audioManager.PlayMusic(loseMusic);
      yield return new WaitForSeconds(loseMusic.length);
      Debug.Break(); // IR A PANTALLA DE DERROTA / FINAL MALO
    }
  }
  public void OnPlayerDeath()
  {
    playerDied = true;
  }

  public void OnLevelFinished()
  {
    levelFinished = true;
  }
}
