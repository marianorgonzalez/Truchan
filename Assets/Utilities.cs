using UnityEngine;
using System.Collections.Generic;
public static class Utilities
{
  public static TruchanButton[] GetAllButtons()
  {
    return Object.FindObjectsByType<TruchanButton>(FindObjectsSortMode.None);
  }

  public static void DealDamageToPlayer(int damage)
  {
    var playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
    if (playerHealth != null)
    {
      playerHealth.DealDamage(damage);
    }
  }

}
