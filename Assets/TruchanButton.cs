using UnityEngine;
using UnityEngine.Events;

public class TruchanButton : MonoBehaviour
{
  [SerializeField] public KeyCode keyboardButton;
  [HideInInspector] public float duration;
  float timeCreated;
  [HideInInspector] public int damage;

    public Animator anim;
    public ParticleSystem pressParticle;

  private void Awake()
  {
    timeCreated = Time.time;
        if (anim == null)
            anim = GetComponent<Animator>();
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
        anim.SetTrigger("pressDissapear");
        pressParticle.Play();
        Destroy(gameObject, 1f);
  }
}
