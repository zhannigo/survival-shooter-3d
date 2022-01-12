using CodeBase.Data;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoadService;
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
      _services.RegisterSingle<IAssets>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService
        (_services.Single<IPersistentProgressService>(),_services.Single<IGameFactory>()));
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
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