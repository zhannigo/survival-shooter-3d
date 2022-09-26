using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.SaveLoadService;
using UnityEngine;

namespace CodeBase.Logic.MenuLogic
{
  public class BuySkinService : MonoBehaviour, ISavedProgress
  {
    protected PlayerProgress Progress;
    protected Dictionary<string, int> SkinsPrice => _cameraOnHero.SkinsPrice;
    protected string SkinName;
    protected bool Selected;
    protected bool Buing;

    private CameraOnSelectedHero _cameraOnHero;
    private ISaveLoadService _saveLoadService;

    public void Construct(PlayerProgress playerProgress, CameraOnSelectedHero cameraOnHero)
    {
      Progress = playerProgress;
      _cameraOnHero = cameraOnHero;
      
      UpdateButtons();
      _cameraOnHero.SkinChanged += UpdateButtons;
    }

    private void Awake() => 
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

    private void UpdateButtons()
    {
      Buing = false;
      Selected = false;
      
      SkinName = _cameraOnHero.SelectedHeroName;
      UpdateBuyButton();
      UpdateSelectButton();
    }

    private void UpdateBuyButton()
    {
      if (!Progress.SkinsData.BuyingSkins.Contains(SkinName))
      {
        BuyButtonOn();
        CanBuy();
      }
      else
      {
        BuyButtonOff();
      }
    }

    private void UpdateSelectButton()
    {
      if (SkinName != Progress.SkinsData.selectedHero)
      {
        SelectButtonOn();
        CanSelect();
      }
      else
      {
        SelectButtonOff();
      }
    }

    protected void SaveProgress() => 
      _saveLoadService.SaveProgress();

    protected virtual void SelectButtonOff() { }

    protected virtual void SelectButtonOn() { }

    protected virtual void BuyButtonOn() { }

    protected virtual void BuyButtonOff() { }

    protected virtual void CanBuy() { }

    protected virtual void CanSelect() { }
    public void UpdateProgress(PlayerProgress progress)
    {
      if (Buing)
      {
        progress.SkinsData.BuyingSkins.Add(SkinName);
      }

      if (Selected)
      {
        progress.SkinsData.selectedHero = SkinName;
      }
    }
    public void LoadProgress(PlayerProgress progress) { }
  }
}

