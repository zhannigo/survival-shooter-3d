using System;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroHealth))]
  public class HeroDeath : MonoBehaviour
  {
    private HeroHealth _heroHealth;
    private HeroMove

    private void OnDestroy() => 
        _heroHealth.healthChanged -= HealthChanged;

    private void Start() => 
        _heroHealth.healthChanged += HealthChanged;

    private void HealthChanged()
    {
      if (_heroHealth.Current <= 0)
        Die();
    }

    private void Die()
    {
      throw new NotImplementedException();
    }
  }
}