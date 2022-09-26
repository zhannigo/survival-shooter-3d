using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.MenuLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class MenuLoadState : IState
  {
    private string menu = "Menu";
    private SceneLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private LoadingCurtain _curtain;
    private IGameFactory _gameFactory;
    private IStaticDataService _staticData;
    private IPersistentProgressService _progressService;

    public MenuLoadState(SceneLoader sceneLoader, IUIFactory uiFactory, LoadingCurtain curtain, 
      IGameFactory gameFactory, IStaticDataService staticData, IPersistentProgressService progressService)
    {
      _sceneLoader = sceneLoader;
      _uiFactory = uiFactory;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _progressService = progressService;
    }

    public void Enter ()
    {
      _curtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmUp();
      _sceneLoader.Load(menu, OnLoaded);
    }

    public void Exit()
    {
    }

    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitialSkins();
      
      _curtain.Hide();
    }
    
    private async Task InitUIRoot() => 
      await _uiFactory.CreateUIRoot();

    private async Task InitialSkins()
    {
      await InitHeroSkins();
      CameraOnSelectedHero().Initialize(_progressService.Progress.SkinsData.selectedHero);
      await InitHud(CameraOnSelectedHero());
    }

    private async Task InitHud(CameraOnSelectedHero cameraOnHero)
    {
      GameObject hud = await _gameFactory.CreateMenuHud();
      
      hud.GetComponent<CameraPickHero>().Construct(cameraOnHero);
      hud.GetComponentInChildren<BuySkinButton>().Construct(_progressService.Progress, cameraOnHero);
      hud.GetComponentInChildren<SelectButton>().Construct(_progressService.Progress, cameraOnHero);
    }

    private async Task InitHeroSkins()
    {
      CameraOnSelectedHero().TargetsTransform = new List<Transform>();
      CameraOnSelectedHero().Targets = new Dictionary<string, Transform>();
      CameraOnSelectedHero().SkinsPrice = new Dictionary<string, int>();
      
      Vector3 position = new Vector3(0,0,0);
      
      foreach (var skin in SkinsStaticData())
      {
        Task<Transform> skinPrefab = _gameFactory.SkinsCreate(skin.Prefab, GetNewPosition(position));
        
        CameraOnSelectedHero().TargetsTransform.Add(await skinPrefab);
        CameraOnSelectedHero().Targets.Add(skin._skinsType.ToString(), await skinPrefab);
        CameraOnSelectedHero().SkinsPrice.Add(skin._skinsType.ToString(), skin._price);
      }
    }
    private Vector3 GetNewPosition(Vector3 position)
    { 
      position.x += 0.5f;
      return position;
    }
    
    private CameraOnSelectedHero CameraOnSelectedHero() => 
      Camera.main.GetComponent<CameraOnSelectedHero>();

    private List<SkinsStaticData> SkinsStaticData()
    {
      List<SkinsStaticData> skinsData = _staticData.HeroSkins();
      return skinsData;
    }
  }
}