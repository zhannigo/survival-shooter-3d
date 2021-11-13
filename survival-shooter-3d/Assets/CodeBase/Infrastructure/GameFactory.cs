using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameFactory : IGameFactory
  {
    private const string HeroPath = "Hero/Hero";
    private const string HudPath = "Hud/Hud";

    public GameObject HeroCreate(GameObject at) =>
      Instanstiate(HeroPath, at.transform.position);

    private static GameObject Instanstiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
    private static GameObject Instanstiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public void CreateHub()
    {
      Instanstiate(HudPath);
    }
  }
}