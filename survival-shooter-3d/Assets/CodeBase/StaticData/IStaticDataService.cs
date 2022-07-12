using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;

namespace CodeBase.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadMonsters();
    MonsterStaticData ForMonster(MonsterTypeId monsterType);
    LevelStaticData ForLevel(string levelKey);
  }
}