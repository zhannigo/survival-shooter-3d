using CodeBase.Infrastructure.AssetManager;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public GameObject HeroCreate(GameObject at) => 
      _assets.Instanstiate(AssetPath.HeroPath, at.transform.position);

    public void CreateHub() => 
      _assets.Instanstiate(AssetPath.HudPath);
  }
}