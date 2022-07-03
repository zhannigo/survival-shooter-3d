using System;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper BootstrapPrefab;
    private void Awake()
    {
      var bootstrapper = FindObjectOfType<GameBootstrapper>();
      if (bootstrapper == null)
        Instantiate(BootstrapPrefab);
    }
  }
}