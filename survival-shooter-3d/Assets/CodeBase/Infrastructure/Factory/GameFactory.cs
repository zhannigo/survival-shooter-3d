using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManager;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject HeroCreate(GameObject at) => 
      InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

    public void CreateHub() => 
      InstantiateRegistered(AssetPath.HudPath);

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instanstiate(prefabPath, at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instanstiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }
      ProgressReaders.Add(progressReader);
    }
  }
}