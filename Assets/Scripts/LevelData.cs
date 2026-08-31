using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
  public AudioClip music;
  public ButtonWaveCollection waves;
  public GameObject truchanPrefab;
  public LevelData nextLevel;
}
