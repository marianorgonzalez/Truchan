using System;
using System.Collections.Generic;

[Serializable]
public class ButtonWave
{
  public bool spawnRandomButtons = true;
  public int randomButtonAmount = 0;
  public List<TruchanButton> specificButtons = new();
  public float timeToPressAllButtons = 0;
  public float timeUntilNextWave = 0;
  public int buttonDamage = 10;
}
