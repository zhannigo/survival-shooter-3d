using CodeBase.Cameralogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LevelLoadState:IPayLoadedState<string>

  {
    private const string InitialpointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LevelLoadState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
      InitialGameWorld();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InitialGameWorld()
    {
      GameObject hero = _gameFactory.HeroCreate(GameObject.FindWithTag(InitialpointTag));
      _gameFactory.CreateHub();
      CameraFollow(hero);
    }

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
    
    public void Exit()
    {
      _curtain.Hide();
    }
  }
}