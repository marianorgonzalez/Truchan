using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonWave", menuName = "Scriptable Objects/ButtonWave")]
public class ButtonWave : ScriptableObject
{
  public bool spawnRandomButtons;
  public int randomButtonAmount;
  public List<TruchanButton> specificButtons;
  public float timeToPressAllButtons;
  public float timeUntilNextWave;
  public int buttonDamage;
}
