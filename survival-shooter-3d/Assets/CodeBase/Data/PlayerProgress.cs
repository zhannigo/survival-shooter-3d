using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public State HeroState;
    public Stats HeroStats;
    public KillData KillData;
    public SkinsData SkinsData;


    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      HeroState = new State();
      HeroStats = new Stats();
      KillData = new KillData();
      SkinsData = new SkinsData();
    }
  }
}