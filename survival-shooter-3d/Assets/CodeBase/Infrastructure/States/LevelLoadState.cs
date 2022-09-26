using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Cameralogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LevelLoadState : IPayLoadedState<string>

  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    public LevelLoadState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
      IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticData;
      _uiFactory = uiFactory;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _curtain.Hide();
    }

    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitialGameWorld();
      InformProgressReaders();
      _curtain.Hide();
    }

    private async Task InitUIRoot() => 
      await _uiFactory.CreateUIRoot();

    private async Task InitialGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      
      await InitEnemy(levelData);
      GameObject hero = await InitHero(levelData);
      await InitHud(hero);
      
      CameraFollow(hero);
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgress progressReader in _gameFactory.ProgressReaders) {
        progressReader.LoadProgress(_progressService.Progress);
      }
    }

    private async Task InitEnemy(LevelStaticData levelData)
    {
      foreach (var spawner in levelData.Spawners)
      {
        await _gameFactory.CreateSpawner(spawner._position, spawner._monsterType, spawner._id);
      }
    }

    private async Task<GameObject> InitHero(LevelStaticData levelData)
    {
      var prefab = SkinsStaticData().Find(x => x._skinsType.ToString() == _progressService.Progress.SkinsData.selectedHero).Prefab;
      return await _gameFactory.HeroCreate(prefab, levelData.InitialPointPosition);
    }

    private async Task InitHud(GameObject hero)
    {
      GameObject hud = await _gameFactory.CreateHud();
      hud.GetComponentInChildren<ActorUI>().Construct(hero.transform.GetComponent<HeroHealth>());
    }

    private LevelStaticData LevelStaticData()
    {
      var sceneKey = SceneManager.GetActiveScene().name;
      LevelStaticData levelData = _staticData.ForLevel(sceneKey);
      return levelData;
    }
    private List<SkinsStaticData> SkinsStaticData()
    {
      List<SkinsStaticData> skinsData = _staticData.HeroSkins();
      return skinsData;
    }

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}