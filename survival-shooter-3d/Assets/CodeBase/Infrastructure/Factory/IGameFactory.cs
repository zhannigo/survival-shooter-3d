using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    GameObject HeroCreate(GameObject at);
    GameObject CreateHud();
    void CreateSpawner(Vector3 at, MonsterTypeId monsterType, string spawnerID);
    void Cleanup();
    GameObject CreateMonster(MonsterTypeId monsterType, Transform parent);
    LootPiece CreateLoot(Vector3 transformPosition);
  }
}