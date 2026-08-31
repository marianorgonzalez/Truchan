using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ButtonWaveCollection", menuName = "Scriptable Objects/ButtonWaveCollection")]
public class ButtonWaveCollection : ScriptableObject
{
  public List<ButtonWave> waves;
}
