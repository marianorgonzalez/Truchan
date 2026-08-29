using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] int maxHealth = 100;
  [SerializeField] Image healthBar;
  [SerializeField] Image portrait;
  [SerializeField] Sprite highHealthSprite;
  [SerializeField] Sprite lowHealthSprite;
  [SerializeField] Sprite damagedSprite;
  [SerializeField] Sprite deathSprite;
  [SerializeField] float damagePortraitDuration = 1f;
  int currentHealth;
  float lastDamageTime = Mathf.NegativeInfinity;

  private void Awake()
  {
    currentHealth = maxHealth;
  }
  private void Update()
  {
    UpdateHealthUI();
  }

  public void DealDamage(int damage)
  {
    currentHealth -= damage;
    currentHealth = Math.Max(currentHealth, 0);
    lastDamageTime = Time.time;
  }

  private void UpdateHealthUI()
  {
    healthBar.fillAmount = Mathf.InverseLerp(0, maxHealth, currentHealth);
    if (Time.time - lastDamageTime < damagePortraitDuration)
    {
      portrait.sprite = damagedSprite;
    }
    else if (currentHealth > maxHealth / 2)
    {
      portrait.sprite = highHealthSprite;
    }
    else if (currentHealth > 0)
    {
      portrait.sprite = lowHealthSprite;
    }
    else
    {
      portrait.sprite = deathSprite;
    }
  }
}
