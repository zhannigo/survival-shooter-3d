using System;
using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CodeBase.Enemy
{
  public class LootPiece: MonoBehaviour
  {
    public GameObject LootObject;
    public GameObject PickUpFx;
    public GameObject PickupPopup;
    public TextMeshPro lootValueText;
    
    private bool _picked;
    private Loot _loot;
    private WorldData _worldData;

    public void Construct(WorldData worldData) => 
      _worldData = worldData;

    public void Initialize(Loot loot) => 
      _loot = loot;

    private void OnTriggerEnter(Collider other) => 
      PickUp();

    private void PickUp()
    {
      if(_picked)
        return;
      _picked = true;
      
      UpdateWorldData();
      HideLoot();
      ShowText();
      PlayFx();
      StartCoroutine(StartDestroyTime());
    }

    private void UpdateWorldData() => 
      _worldData.LootData.Collect(_loot);

    private void HideLoot() => 
      LootObject.SetActive(false);

    private void PlayFx() => 
      Instantiate(PickUpFx, transform.position, Quaternion.identity, gameObject.transform);

    private void ShowText()
    {
      lootValueText.text = $"{_loot.Value}";
      PickupPopup.SetActive(true);
    }

    private IEnumerator StartDestroyTime()
    {
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
    }
  }
}