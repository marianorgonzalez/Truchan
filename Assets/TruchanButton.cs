using UnityEngine;
using UnityEngine.Events;

public class TruchanButton : MonoBehaviour
{
  [SerializeField] public KeyCode keyboardButton;
  [HideInInspector] public float duration;
  float timeCreated;
  [HideInInspector] public int damage;

  private void Awake()
  {
    timeCreated = Time.time;
  }
  private void Update()
  {
    if (Time.time - timeCreated > duration)
    {
      Utilities.DealDamageToPlayer(damage);
      Destroy(gameObject);
    }
  }
  public void OnPressed()
  {
    Destroy(gameObject);
  }
}
