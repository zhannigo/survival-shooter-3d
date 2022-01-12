using UnityEngine;

namespace CodeBase.Infrastructure.AssetManager
{
  public interface IAssets
  {
    GameObject Instanstiate(string path);
    GameObject Instanstiate(string path, Vector3 at);
  }
}