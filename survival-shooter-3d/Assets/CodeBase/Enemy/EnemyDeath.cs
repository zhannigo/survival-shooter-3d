using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class EnemyDeath : MonoBehaviour
  {
    public EnemyHealth _health;
    public EnemyAnimator _animator;
    public GameObject DeathFx;
    public event Action EnemyDead;

    private void OnDestroy() => 
        _health.HealthChanged -= HealthChanged;

    private void Start() => 
        _health.HealthChanged += HealthChanged;

    private void HealthChanged()
    {
      if (_health.CurrentHp <= 0)
        Die();
    }

    private void Die()
    {
      _animator.PlayDeathEnemy();

      Instantiate(DeathFx, transform.position, Quaternion.identity);
      StartCoroutine(TimeToDeath());
      EnemyDead?.Invoke();
      
      _health.HealthChanged -= HealthChanged;
    }

    private IEnumerator TimeToDeath()
    {
      yield return new WaitForSeconds(3f);
      Destroy(gameObject);
    }
  }
}