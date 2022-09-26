using System;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroHealth))]
  public class HeroDeath : MonoBehaviour
  {
    public HeroHealth _heroHealth;
    public HeroMove _heroMove;
    public HeroAnimator _heroAnimator;
    public HeroAttack _heroAttack;
    public GameObject DeathFx;
    public Action HeroDead;
    private bool _isDead = false;

    private void OnDestroy() => 
        _heroHealth.HealthChanged -= HealthChanged;

    private void Start() => 
        _heroHealth.HealthChanged += HealthChanged;

    private void HealthChanged()
    {
      if (!_isDead && _heroHealth.CurrentHp <= 0)
        Die();
    }

    private void Die()
    {
      _isDead = true;
      _heroMove.enabled = false;
      _heroAttack.enabled = false;
      _heroAnimator.PlayDeath();

      Instantiate(DeathFx, transform.position, Quaternion.identity);
      HeroDead?.Invoke();
    }
  }
}