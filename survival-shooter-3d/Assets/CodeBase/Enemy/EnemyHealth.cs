using System;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(Animator))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    [SerializeField]
    private float _maxHp;
    
    [SerializeField]
    private float _currentHp;
    
    public EnemyAnimator Animator;
    public event Action HealthChanged;
    public float CurrentHp
    {
      get => _currentHp;
      set => _currentHp = value;
    }
    public float MaxHp
    {
      get => _maxHp;
      set => _maxHp = value;
    }

    public void TakeDamage(float damage)
    {
      CurrentHp -= damage;
      Animator.PlayHit();
      
      HealthChanged?.Invoke();
    }
  }
}