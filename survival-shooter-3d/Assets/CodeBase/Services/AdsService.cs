using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services
{
  public class AdsService : IUnityAdsListener, IAdsService
  {
    private const string AndroidGameID = "4847800";
    private const string IOSGameId = "4847801";
    private const string RevardedVideoPlacementId = "revardedVideo";
    
    private string _gameId;
    private Action _onVideoFinished;
    public event Action RevardedVideoReady;

    public int Reward => 10;

    public void Initialize()
    {
      switch (Application.platform)
      {
        case RuntimePlatform.Android:
          _gameId = AndroidGameID;
          break;
        case  RuntimePlatform.IPhonePlayer:
          _gameId = IOSGameId;
          break;
        case RuntimePlatform.WindowsEditor:
          _gameId = AndroidGameID;
          break;
        default:
          Debug.Log("Unsupported platform for ads");
          break;
      }
      Advertisement.AddListener(this);
      Advertisement.Initialize(_gameId);
    }

    public bool IsRevardedVideoReady => 
      Advertisement.IsReady(RevardedVideoPlacementId);

    public void ShowRevardedVideo(Action onVideoFinished)
    {
      Advertisement.Show(RevardedVideoPlacementId);
      _onVideoFinished = onVideoFinished;
    }
    public void OnUnityAdsReady(string placementId)
    {
      Debug.Log($"OnUnityAdsReady {placementId}");
      
      if(RevardedVideoPlacementId == placementId)
        RevardedVideoReady?.Invoke();
    }
    
    public void OnUnityAdsDidError(string message) => 
      Debug.Log($"OnUnityAdsDidError {message}");

    public void OnUnityAdsDidStart(string placementId) => 
      Debug.Log($"OnUnityAdsDidStart {placementId}");

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
      switch (showResult)
      {
        case ShowResult.Failed:
          Debug.Log($"OnUnityAdsDidFinish {showResult}");
          break;
        case ShowResult.Skipped:
          Debug.Log($"OnUnityAdsDidFinish {showResult}");
          break;
        case ShowResult.Finished:
          _onVideoFinished?.Invoke();
          break;
        default:
          Debug.Log($"OnUnityAdsDidFinish {showResult}");
          break;
      }
      _onVideoFinished = null;
    }
  }
}