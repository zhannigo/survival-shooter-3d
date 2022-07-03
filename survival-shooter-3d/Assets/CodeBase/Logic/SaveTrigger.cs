using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.SaveLoadService;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic
{
  public class SaveTrigger: MonoBehaviour
  {
    private ISaveLoadService _saveLoadService;
    public BoxCollider Collider;

    private void Awake()
    {
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter(Collider other)
    {
      _saveLoadService.SaveProgress();
      Debug.Log("Saved Progress");
      gameObject.SetActive(false);
    }

    public void OnDrawGizmos()
    {
      if (!Collider)
        return;
      
      Gizmos.color = new Color32(30, 80, 180, 130);
      Gizmos.DrawCube(transform.position+ Collider.center, Collider.size);
    }
  }
}