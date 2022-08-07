using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Logic
{
  public class LevelTransfer: MonoBehaviour
  {
    public string TransferTo;
    private IGameStateMachine _stateMashine;
    private bool _isTrigerred;

    public void Awake() => 
      _stateMashine = AllServices.Container.Single<IGameStateMachine>();

    private void OnTriggerEnter(Collider other)
    {
      if(_isTrigerred)
        return;
    
      if (other.CompareTag("Player"))
      {
        _stateMashine.Enter<LevelLoadState, string>(TransferTo);
        _isTrigerred = true;
      }
    }
  }
}