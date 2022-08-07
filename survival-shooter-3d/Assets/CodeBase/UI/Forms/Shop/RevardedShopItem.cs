using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Forms.Shop
{
  public class RevardedShopItem : MonoBehaviour
  {
    public Button _revardedItemButton;
    public GameObject[] AdsActiveObject;
    public GameObject[] AdsInActiveObject;
    private IAdsService _adsService;
    private PlayerProgress _progress;
  
    public void Construct(IAdsService adsService, PlayerProgress progress)
    {
      _adsService = adsService;
      _progress = progress;
    }
  
    public void Initialize()
    {
      _adsService.Initialize();
      _revardedItemButton.onClick.AddListener(OnShowAdClicked);
      RefreshRevardedAds();
    }
  
    private void OnShowAdClicked() => 
      _adsService.ShowRevardedVideo(onVideoFinished);
  
    public void Subscribe() => 
      _adsService.RevardedVideoReady += RefreshRevardedAds;
  
    public void Cleanup() => 
      _adsService.RevardedVideoReady -= RefreshRevardedAds;
  
    private void onVideoFinished() => 
      _progress.WorldData.LootData.Add(_adsService.Reward);
  
    private void RefreshRevardedAds()
    {
      bool videoReady = _adsService.IsRevardedVideoReady;
      foreach (GameObject activeObject in AdsActiveObject)
      {
        activeObject.SetActive(videoReady);
      }
      foreach (GameObject inactiveObject in AdsInActiveObject)
      {
        inactiveObject.SetActive(!videoReady);
      }
    }
  }
}
