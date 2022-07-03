using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class Aggro: MonoBehaviour
  {
    public TriggerObserver TriggerObserver;
    public AgentMoveToPlayer Follow;
    
    private float _cooldown;
    private Coroutine _aggroCoroutine;
    private bool _hasAggroTarget;

    private void Start()
    {
      TriggerObserver.TriggerEnter += OnTriggerEnter;
      TriggerObserver.TriggerExit += OnTriggerExit;
      SwitchFollowOff();
    }

    private void OnTriggerEnter(Collider obj)
    {
      if (!_hasAggroTarget)
      {
        _hasAggroTarget = true;
        SwitchFollowOn();
        StopAggroCoroutine();
      }
    }

    private void OnTriggerExit(Collider obj)
    {
      if (_hasAggroTarget)
      {
        _hasAggroTarget = false;
        _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
      }
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
      yield return new WaitForSeconds(_cooldown);
      SwitchFollowOff();
    }

    private void StopAggroCoroutine()
    {
      if (_aggroCoroutine != null)
      {
        StopCoroutine(_aggroCoroutine);
        _aggroCoroutine = null;
      }
    }

    private void SwitchFollowOn() => 
      Follow.enabled = true;

    private void SwitchFollowOff() => 
      Follow.enabled = false;
  }
}