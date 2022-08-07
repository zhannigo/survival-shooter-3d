using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetManager
{
  public class AssetProvider : IAssets
  {
    private Dictionary<string, AsyncOperationHandle> _complitedHandle = new Dictionary<string, AsyncOperationHandle>();
    private Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

    public void Initialize() => 
      Addressables.InitializeAsync();

    public async Task<T> Load<T>(AssetReference assetReference) where T : class
    {
      if (_complitedHandle.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle complitedHandle))
         return complitedHandle.Result as T; 

      return await RunWithCashOnCompleted(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
    }

    public async Task<T> Load<T>(string address) where T : class
    {
      if (_complitedHandle.TryGetValue(address, out AsyncOperationHandle complitedHandle))
        return complitedHandle.Result as T;

      return await RunWithCashOnCompleted(Addressables.LoadAssetAsync<T>(address), address);
    }

    public Task<GameObject> Instanstiate(string address) => 
      Addressables.InstantiateAsync(address).Task;

    public Task<GameObject> Instanstiate(string address, Vector3 at)
    {
      return Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;
    }

    public void Cleanup()
    {
      foreach (List<AsyncOperationHandle> handleList in _handles.Values)
      foreach (AsyncOperationHandle handle in handleList)
        Addressables.Release(handle);
      
      _handles.Clear();
      _complitedHandle.Clear();
    }

    private void AddHandles<T>(string key, AsyncOperationHandle<T> handle) where T : class
    {
      if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
      {
        resourceHandle = new List<AsyncOperationHandle>();
        _handles[key] = resourceHandle;
      }

      resourceHandle.Add(handle);
    }

    private async Task<T> RunWithCashOnCompleted<T>(AsyncOperationHandle<T> handle, string cashKey) where T : class
    {
      handle.Completed += completeHandle =>
      {
        _complitedHandle[cashKey] = completeHandle;
      };

      AddHandles(cashKey, handle);

      return await handle.Task;
    }
  }
}