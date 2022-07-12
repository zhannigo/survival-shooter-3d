using System;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.StaticData
{
  [Serializable]
  public class EnemySpawnerData
  {
    public string _id;
    public MonsterTypeId _monsterType;
    public Vector3 _position;

    public EnemySpawnerData(string id, MonsterTypeId monsterType, Vector3 position)
    {
      _id = id;
      _monsterType = monsterType;
      _position = position;
    }
  }
}