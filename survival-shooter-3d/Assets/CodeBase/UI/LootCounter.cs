using System;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
  public class LootCounter : MonoBehaviour
  {
    public TextMeshProUGUI _textCounter;
    private LootData _lootData;

    public void Construct(LootData lootData)
    {
      _lootData = lootData;
      _lootData.ChangedCollected += UpdateCounter;
    }
    private void Start()
    {
      UpdateCounter();
    }

    private void UpdateCounter()
    {
      _textCounter.text = $"{_lootData.Collected}";
    }
  }
}
