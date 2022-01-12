using System;
using CodeBase.Cameralogic;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using static CodeBase.Infrastructure.Factory.GameFactory;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
  public class LevelLoadState:IPayLoadedState<string>

  {
    private const string InitialpointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly GameFactory _gameFactory;

    public LevelLoadState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
      var initialPoint = GameObject.FindWithTag(InitialpointTag);
      GameObject hero = _gameFactory.HeroCreate(initialPoint);
      _gameFactory.CreateHub();
      CameraFollow(hero);
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
    
    public void Exit()
    {
      _curtain.Hide();
    }
  }
}