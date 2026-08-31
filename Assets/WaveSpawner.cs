using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
  [SerializeField] ButtonWaveCollection wavesCollection;
  [SerializeField] List<BoxCollider2D> buttonSpawnZones;
  float lastWaveTimer = Mathf.NegativeInfinity;
  int currentWave = 0;
  List<ButtonWave> waves;
  [SerializeField] List<TruchanButton> randomButtonPool;
  [SerializeField] UnityEvent OnWaveFinished;
  List<TruchanButton> currentWaveButtons = new();

  private void Awake()
  {
    waves = wavesCollection.waves;
  }

  private void Update()
  {
    if (currentWaveButtons.Any() == false && Time.time >= lastWaveTimer)
    {
      SpawnButtonWave(waves[currentWave]);
    }
  }

  private void OnAllWavesFinished()
  {
    Debug.Break();
    Debug.Log("Level finished!");
    
  }

  private void SpawnButtonWave(ButtonWave buttonWave)
  {
    currentWaveButtons = new();
    if (buttonWave.spawnRandomButtons)
    {
      List<int> spawnedButtons = new();
      for (int i = 0; i < buttonWave.randomButtonAmount; i++)
      {
        int? randomButtonIndex = null;
        while (randomButtonIndex.HasValue == false || spawnedButtons.Contains(randomButtonIndex.Value))
        {
          randomButtonIndex = Random.Range(0, randomButtonPool.Count);
        }
        spawnedButtons.Add(randomButtonIndex.Value);
        if (spawnedButtons.Count == randomButtonPool.Count)
          spawnedButtons.Clear();
        SpawnButton(randomButtonPool[randomButtonIndex.Value]);
      }
    }
    else
    {
      foreach (var button in buttonWave.specificButtons)
      {
        SpawnButton(button);
      }
    }
  }

  private void SpawnButton(TruchanButton buttonPrefab)
  {
    var spawnedButton = Instantiate(buttonPrefab);
    spawnedButton.duration = waves[currentWave].timeToPressAllButtons;
    spawnedButton.damage = waves[currentWave].buttonDamage;
    int iterations = 0;
    while (iterations < 15 && IsButtonOverlappingOtherButtons(spawnedButton))
    {
      spawnedButton.transform.position = GetRandomPositionInBounds(buttonSpawnZones[Random.Range(0, buttonSpawnZones.Count)].bounds);
      iterations++;
    }
    if (iterations == 15)
      Debug.LogWarning("Iterations exceeded when spawning button");
    currentWaveButtons.Add(spawnedButton);
    spawnedButton.OnButtonDestroyed.AddListener(OnButtonDestroyed);
  }

  private void OnButtonDestroyed(TruchanButton obj)
  {
    currentWaveButtons.Remove(obj);
    if (currentWaveButtons.Count == 0)
    {
      OnWaveFinished.Invoke();
      lastWaveTimer = waves[currentWave].timeUntilNextWave + Time.time;
      currentWave++;
      if (currentWave >= waves.Count)
      {
        OnAllWavesFinished();
      }
    }
  }

  private bool IsButtonOverlappingOtherButtons(TruchanButton button)
  {
    var sprite = button.GetComponent<SpriteRenderer>();
    var buttonsInScene = Object.FindObjectsByType<TruchanButton>(FindObjectsSortMode.InstanceID).Where(x => !x.Equals(button));
    foreach (var buttonInScene in buttonsInScene)
    {
      var buttonInSceneSprite = buttonInScene.GetComponent<SpriteRenderer>();
      if (sprite.bounds.Intersects(buttonInSceneSprite.bounds))
      {
        return true;
      }
    }
    return false;
  }


  private Vector3 GetRandomPositionInBounds(Bounds bounds)
  {
    return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
  }

  private void OnButtonDestroyed()
  {

  }
}
