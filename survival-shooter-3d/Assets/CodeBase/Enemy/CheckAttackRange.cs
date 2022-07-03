using System;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class CheckAttackRange:MonoBehaviour
  {
    public Attack Attack;
    public TriggerObserver TriggerObserver;

    private void Awake()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerExit += TriggerExit;

      Attack.DisableAttack();
    }

    private void TriggerExit(Collider obj)
    {
      Attack.EnableAttack();
    }

    private void TriggerEnter(Collider obj)
    {
      Attack.EnableAttack();
    }
  }
}