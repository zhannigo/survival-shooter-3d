using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Levels")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerData> Spawners;
  }
}