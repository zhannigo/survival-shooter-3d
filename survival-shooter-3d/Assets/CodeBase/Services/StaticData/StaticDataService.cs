using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataMonstersPath = "StaticData/Monsters";
    private const string StaticDataLevelsPath = "StaticData/Levels";
    private const string StaticDataUIWindowsPath = "StaticData/UI/WindowData";
    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowsConfig> _windowsConfigs;

    public void LoadMonsters()
    {
      _monsters = Resources.LoadAll<MonsterStaticData>(StaticDataMonstersPath)
        .ToDictionary(x => x.MonsterType, x => x);
      _levels = Resources.LoadAll<LevelStaticData>(StaticDataLevelsPath)
        .ToDictionary(x => x.LevelKey, x => x);
      _windowsConfigs = Resources.Load<WindowsStaticData>(StaticDataUIWindowsPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
    }

    public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId)
    {
      return _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData)
        ? staticData
        : null;
    }

    public LevelStaticData ForLevel(string levelKey)
    {
      return _levels.TryGetValue(levelKey, out LevelStaticData levelData)
        ? levelData
        : null;
    }

    public WindowsConfig ForWindow(WindowId windowId)
    {
      return _windowsConfigs.TryGetValue(windowId, out WindowsConfig config) 
        ? config 
        : null;
    }
  }
}