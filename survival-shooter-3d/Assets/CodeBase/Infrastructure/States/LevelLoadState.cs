using System;
using CodeBase.Cameralogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LevelLoadState : IPayLoadedState<string>

  {
    private const string InitialpointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private IStaticDataService _staticData;

    public LevelLoadState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
      IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticData;
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
      InformProgressReaders();
      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgress progressReader in _gameFactory.ProgressReaders) {
        progressReader.LoadProgress(_progressService.Progress);
      }
      
    }

    private void InitialGameWorld()
    {
      InitEnemy();
      GameObject hero = InitHero();
      InitHud(hero);
      CameraFollow(hero);
    }

    private void InitEnemy()
    {
      var sceneKey = SceneManager.GetActiveScene().name;
      LevelStaticData levelData = _staticData.ForLevel(sceneKey);
      foreach (var spawner in levelData.Spawners)
      {
        _gameFactory.CreateSpawner(spawner._position, spawner._monsterType, spawner._id);
      }
    }

    private GameObject InitHero()
    {
      return _gameFactory.HeroCreate(GameObject.FindGameObjectWithTag(InitialpointTag));
    }

    private void InitHud(GameObject hero)
    {
      GameObject hud = _gameFactory.CreateHud();
      hud.GetComponentInChildren<ActorUI>().Construct(hero.transform.GetComponent<HeroHealth>());
    }

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);

    public void Exit()
    {
      _curtain.Hide();
    }
  }
}