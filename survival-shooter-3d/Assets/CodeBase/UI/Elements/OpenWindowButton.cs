using System;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class OpenWindowButton : MonoBehaviour
  {
    public Button _button;
    public WindowId _windowId;
    
    private IWindowService _windowService;
    public void Construct(IWindowService windowService) => 
      _windowService = windowService;
    private void Awake() => 
      _button.onClick.AddListener(Open);
    private void Open() => 
      _windowService.Open(_windowId);
  }
}