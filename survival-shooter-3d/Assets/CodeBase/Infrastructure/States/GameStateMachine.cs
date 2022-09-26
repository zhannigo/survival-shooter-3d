using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LevelLoadState)] = new LevelLoadState(this, sceneLoader, curtain, services.Single<IGameFactory>(), 
          services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>(), 
          services.Single<IUIFactory>()),
        [typeof(LoadProgressState)] = new LoadProgressState(this, 
          services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
        [typeof(MenuLoadState)] = new MenuLoadState(sceneLoader, services.Single<IUIFactory>(), curtain, 
          services.Single<IGameFactory>(), services.Single<IStaticDataService>(), 
          services.Single<IPersistentProgressService>())
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
    {
      TState state = ChangeState<TState>();
      state.Enter(payLoad);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }

}