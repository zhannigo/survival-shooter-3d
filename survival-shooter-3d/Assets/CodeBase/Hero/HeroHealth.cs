using System;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
  {
    public HeroAnimator Animator;
    private State _state;

    public event Action HealthChanged;
    public float CurrentHp
    {
      get => _state.CurrentHP;
      set
      {
        if (_state.CurrentHP != value) {
          _state.CurrentHP = value;
          HealthChanged?.Invoke();
        }
      }
    }

    public float MaxHp
    {
      get => _state.MaxHP; 
      set => _state.MaxHP = value;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      _state = progress.HeroState;
      HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.HeroState.CurrentHP = CurrentHp;
      progress.HeroState.MaxHP = MaxHp;
    }

    public void TakeDamage(float damage)
    {
      if (CurrentHp <= 0)
        return;
      
      CurrentHp -= damage;
      Animator.PlayHit();
    }
  }
}