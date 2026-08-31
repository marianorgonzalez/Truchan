using System;
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
  public UnityEvent<TruchanButton> OnButtonDestroyed;
  [SerializeField] public AudioClip buttonDestroyedSound;
  private void Awake()
  {
    timeCreated = Time.time;
    if (anim == null)
      anim = GetComponent<Animator>();
  }

  protected virtual void Update()
  {
    if (Time.time - timeCreated > duration)
    {
      Utilities.DealDamageToPlayer(damage);
    }
  }
  public virtual void OnPressed()
  {
        anim.SetTrigger("pressDissapear");
        pressParticle.transform.SetParent(null);
        pressParticle.Play();
        Destroy(pressParticle, 2.5f);
        Destroy(gameObject, .5f);
        OnButtonDestroyed?.Invoke(this);
  }

  private void OnDestroy()
  {
  }
}
