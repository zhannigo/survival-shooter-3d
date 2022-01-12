using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instanstiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instanstiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
  }
}