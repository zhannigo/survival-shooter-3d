using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
  [Serializable]
  public class LootData
  {
    public int Collected;
    public event Action ChangedCollected;

    public void Collect(Loot loot)
    {
      Collected += loot.Value;
      ChangedCollected?.Invoke();
    }
  }
}