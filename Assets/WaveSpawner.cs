using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
  [SerializeField] List<ButtonWave> waves;
  [SerializeField] BoxCollider2D spawnZone1;
  [SerializeField] BoxCollider2D spawnZone2;
  float lastWaveTime = Mathf.NegativeInfinity;
  int currentWave = 0;
  [SerializeField] List<TruchanButton> randomButtonPool;
  private void Update()
  {
    if (Time.time - lastWaveTime > waves[currentWave].timeUntilNextWave)
    {
      SpawnButtonWave(waves[currentWave]);
      lastWaveTime = Time.time;
      currentWave++;
      if (currentWave == waves.Count)
        OnWavesFinished();
    }
  }

  private void OnWavesFinished()
  {
    currentWave = 0;
  }

  private void SpawnButtonWave(ButtonWave buttonWave)
  {
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

  private void SpawnButton(TruchanButton button)
  {
    var spawnedButton = Instantiate(button);
    spawnedButton.duration = waves[currentWave].timeToPressAllButtons;
    spawnedButton.damage = waves[currentWave].buttonDamage;
    int iterations = 0;
    while (iterations < 15 && IsButtonOverlappingOtherButtons(spawnedButton))
    {
      if (Random.Range(0, 101) > 50)
      {
        spawnedButton.transform.position = GetRandomPositionInBounds(spawnZone1.bounds);
      }
      else
      {
        spawnedButton.transform.position = GetRandomPositionInBounds(spawnZone2.bounds);
      }
      iterations++;
    }
    if (iterations == 15)
      Debug.LogWarning("Iterations exceeded when spawning button");
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
        Debug.Log("Solapamiento");
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
}
