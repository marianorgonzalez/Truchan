using UnityEngine;
using System.Collections.Generic;
public static class Utilities
{
  public static TruchanButton[] GetAllButtons()
  {
    return Object.FindObjectsByType<TruchanButton>(FindObjectsSortMode.None);
  }

  public static void DealDamageToPlayer(int damage, bool ignoreInvulnerability = false)
  {
    var playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
    if (playerHealth != null)
    {
      playerHealth.DealDamage(damage, ignoreInvulnerability);
    }
  }
  public static List<T> PickRandomFromList<T>(List<T> originList, int amountToPick)
  {
    var chosenItems = new List<T>();
    while (chosenItems.Count < amountToPick)
    {
      var clip = originList[Random.Range(0, originList.Count)];
      if (chosenItems.Contains(clip) == false || (originList.Count < amountToPick && chosenItems.Count >= originList.Count))
        chosenItems.Add(clip);
    }
    return chosenItems;
  }

  public static void PlaySFX(AudioClip clip, float pitch)
  {
    var audioManager = Object.FindFirstObjectByType<AudioManager>();
    if (audioManager != null)
    {
      audioManager.PlaySFX(clip, pitch);
    }
  }
}
