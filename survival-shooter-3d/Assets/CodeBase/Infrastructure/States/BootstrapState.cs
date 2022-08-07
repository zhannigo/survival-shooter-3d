using CodeBase.Data;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Factory;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState:IState

  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    private const string InitialName = "Initial";

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(InitialName,EnterLoadLevel);
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();

    private void RegisterServices()
    {
      _services.RegisterSingle<IInputService>(InputService());
      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      RegisterAssetProvider();
      IStaticDataService staticData = RegisterStaticData();
      IRandomService randomService = new UnityRandomService();
      
      _services.RegisterSingle(randomService);
      IPersistentProgressService progressService = new PersistentProgressService();
      _services.RegisterSingle(progressService);

      _services.RegisterSingle<IAdsService>(new AdsService());
      _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(), staticData, progressService, _services.Single<IAdsService>()));
      _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

      _services.RegisterSingle<IGameFactory>(new GameFactory
        (_services.Single<IAssets>(), staticData, randomService, progressService, _services.Single<IWindowService>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService
        (_services.Single<IPersistentProgressService>(),_services.Single<IGameFactory>()));
    }

    private void RegisterAssetProvider()
    {
      AssetProvider assetProvider = new AssetProvider();
      assetProvider.Initialize();
      _services.RegisterSingle<IAssets>(assetProvider);
    }

    private IStaticDataService RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      _services.RegisterSingle(staticData);
      staticData.LoadMonsters();
      return staticData;
    }

    public void Exit()
    {
    }

    private static IInputService InputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new MobileInputService();
    }
  }
}