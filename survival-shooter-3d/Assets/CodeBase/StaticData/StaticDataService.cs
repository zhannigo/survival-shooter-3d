using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
    
    public void LoadMonsters()
    {
      _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
        .ToDictionary(x => x.MonsterType, x => x);
    }

    public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId)
    {
      return _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData)
        ? staticData
        : null;
    }
  }
}