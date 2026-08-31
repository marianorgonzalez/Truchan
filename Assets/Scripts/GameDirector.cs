using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
  [SerializeField] LevelData firstLevel;
  [SerializeField] GameObject truchanContainer;
  [SerializeField] ButtonDestroyer buttonDestroyer;
  [SerializeField] WaveSpawner waveSpawner;
  [SerializeField] AudioManager audioManager;
  [SerializeField] float waitBeforeFirstWave = 3f;
  [SerializeField] AudioClip winMusic;
  [SerializeField] AudioClip loseMusic;
  [SerializeField] AudioClip winSfx;
  [SerializeField] AudioClip loseSfx;
  [SerializeField] UnityEvent OnLevelCompleted;
  [SerializeField] LevelTransitions transitions;
  [SerializeField] float timeBetweenLevels = 1.5f;
  [SerializeField] PlayerHealth health;
  bool levelFinished = false;
  bool playerDied = false;

  private void Awake()
  {
    transitions.MakeOverlayOpaque();
    buttonDestroyer.enabled = false;
  }
  private void Start()
  {
    StartCoroutine(ExecuteLevel(firstLevel));
  }

  IEnumerator ExecuteLevel(LevelData level)
  {
    levelFinished = playerDied = false;
    health.RestoreAllHealth();
    // Preparacion de objetos del nivel
    if (truchanContainer.transform.childCount > 0)
      Destroy(truchanContainer.transform.GetChild(0).gameObject);
    var truchanPrefab = Instantiate(level.truchanPrefab, truchanContainer.transform);
    var truchanAnimator = truchanPrefab.GetComponent<Animator>();
    UnityAction resetAction = () => truchanAnimator.SetTrigger("reset");
    waveSpawner.OnWaveFinished.AddListener(resetAction);
    waveSpawner.OnWaveCollectionFinished.AddListener(OnLevelFinished);

    // transicion y empieza nivel
    yield return StartCoroutine(transitions.TransitionIn());
    truchanAnimator.SetBool("start", true);
    audioManager.PlayMusic(level.music);
    yield return new WaitForSeconds(waitBeforeFirstWave);
    buttonDestroyer.enabled = true;
    StartCoroutine(waveSpawner.SpawnWaveCollection(level.waves));

    // esperamos a que el jugador gane o pierda
    yield return new WaitUntil(() => levelFinished || playerDied);
    
    // limpieza
    waveSpawner.OnWaveFinished.RemoveListener(resetAction);
    waveSpawner.OnWaveCollectionFinished.RemoveListener(OnLevelFinished);
    buttonDestroyer.enabled = false;
    audioManager.StopAllMusic();
    if (levelFinished)
    {
      truchanAnimator.SetTrigger("win");
      audioManager.PlaySFX(winMusic);
      audioManager.PlaySFX(winSfx);
      yield return new WaitForSeconds(winMusic.length);
      yield return StartCoroutine(transitions.TransitionOut());
      yield return new WaitForSeconds(timeBetweenLevels);
      if (level.nextLevel == null)
      {
        SceneManager.LoadScene("FinalBueno");
      }
      else
      {
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
        Destroy(button.gameObject);
      }
      audioManager.PlaySFX(loseMusic);
      audioManager.PlaySFX(loseSfx);
      yield return new WaitForSeconds(loseMusic.length);
      yield return StartCoroutine(transitions.TransitionOut());
      SceneManager.LoadScene("FinalMalo");
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
