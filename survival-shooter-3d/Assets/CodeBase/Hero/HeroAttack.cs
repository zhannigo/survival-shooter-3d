using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(CharacterController),typeof(HeroAnimator))]
  public class HeroAttack : MonoBehaviour, ISavedProgress
  {
    public CharacterController controller;
    public HeroAnimator heroAnimator;
    private IInputService _input;
    private Collider[] _hits = new Collider[3]; //enemy collider's count hero can hit
    private int _layerMask;
    private Stats _stats;
    private const string HITTABLE = "Hittable";

    private void Awake()
    {
      _input = AllServices.Container.Single<IInputService>();
      _layerMask = 1 << LayerMask.NameToLayer(HITTABLE);
    }

    private void Update()
    {
      if (!heroAnimator.IsAttacking && _input.IsAttackButtonUp()) {
        heroAnimator.PlayAttack();
      }
    }

    public void LoadProgress(PlayerProgress progress) => 
        _stats = progress.HeroStats;

    private void OnAttack()
    {
      for (int i = 0; i < Hit(); i++) {
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.damage);
      }
    }

    private int Hit() => 
        Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.damageRadius, _hits, _layerMask);

    private Vector3 StartPoint() => 
        new Vector3(transform.position.x, controller.center.y/2, transform.position.z);

    public void UpdateProgress(PlayerProgress progress)
    {
      
    }
  }
}