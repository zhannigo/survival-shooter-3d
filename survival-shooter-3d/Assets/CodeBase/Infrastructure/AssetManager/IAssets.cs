using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManager
{
  public interface IAssets:IService
  {
    GameObject Instanstiate(string path);
    GameObject Instanstiate(string path, Vector3 at);
  }
}