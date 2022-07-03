using System;
using System.Linq;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(Animator))]
  public class Attack:MonoBehaviour
  {
    public EnemyAnimator Animator;
    public float AttackCooldown;
    public float Cleavage;
    public float EffectiveDistance;
    public float Damage;

    private Transform _heroTransform;
    private float _cooldown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private bool _attackIsActive;

    public void Construct(Transform heroTransform) => 
      _heroTransform = heroTransform;

    private void Awake()
    {
      _layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
      UpdateCooldown();
      if(CanAttack())
        StartAttack();
    }

    private void OnAttack()
    {
      if (Hit(out Collider hit))
      {
        PhysicsDebug.DrawDebug(StartPoint(),Cleavage,1);
        hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
      }
    }

    private bool Hit(out Collider hit)
    {
      int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
      hit = _hits.FirstOrDefault();
      return hitCount > 0;
    }

    private void StartAttack()
    {
      transform.LookAt(_heroTransform);
      Animator.PlayAttack();
      _isAttacking = true;
    }

    private void OnAttackEnded()
    {
      _cooldown = AttackCooldown;
      _isAttacking = false;
    }

    private Vector3 StartPoint() => 
      new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z)+transform.forward*EffectiveDistance;

    private bool CooldownIsUp() => 
      _cooldown<=0;

    private bool CanAttack() => 
      !_isAttacking && _attackIsActive && CooldownIsUp();

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _cooldown -= Time.deltaTime;
    }

    public void DisableAttack() => 
      _attackIsActive = false;

    public void EnableAttack() => 
      _attackIsActive = true;
  }
}