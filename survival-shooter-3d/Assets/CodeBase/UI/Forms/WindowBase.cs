using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Forms
{
  public class WindowBase : MonoBehaviour
  {
    protected static IPersistentProgressService ProgressService;
    protected PlayerProgress Progress => ProgressService.Progress;
    public Button CloseButton;

    public void Construct(IPersistentProgressService progressService)
    {
      ProgressService = progressService;
    }

    private void Awake() => 
      OnAwake();

    private void Start()
    {
      InitializeWindow();
      SubscribeUpdate();
    }

    private void OnDestroy() => 
      Cleanup();

    protected virtual void OnAwake() => 
      CloseButton.onClick.AddListener(() => Destroy(gameObject));

    protected virtual void InitializeWindow() {}
    protected virtual void SubscribeUpdate() {}
    protected virtual void Cleanup() {}
  }
}