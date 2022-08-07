using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManager
{
  public interface IAssets:IService
  {
    void Initialize();
    Task<GameObject> Instanstiate(string path);
    Task<GameObject> Instanstiate(string path, Vector3 at);
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    void Cleanup();
    Task<T> Load<T>(string address) where T: class;
  }
}