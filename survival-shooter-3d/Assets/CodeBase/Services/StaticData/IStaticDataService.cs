using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadStaticData();
    MonsterStaticData ForMonster(MonsterTypeId monsterType);
    LevelStaticData ForLevel(string levelKey);
    WindowsConfig ForWindow(WindowId windowId);
    List<SkinsStaticData> HeroSkins();
  }
}