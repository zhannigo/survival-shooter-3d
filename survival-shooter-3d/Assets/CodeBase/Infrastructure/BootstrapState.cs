using System;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class BootstrapState:IState

  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private const string InitialName = "Initial";

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      RegisterServices();
      _sceneLoader.Load(InitialName,EnterLoadLevel);
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LevelLoadState,string>("Main");

    private void RegisterServices()
    {
      Game.InputService = RegisterInputService();
    }

    public void Exit()
    {
    }

    private static IInputService RegisterInputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new MobileInputService();
    }
  }
}