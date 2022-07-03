using System.Collections.Generic;
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
    void Cleanup();
    void Register(ISavedProgressReader progressReader);
    GameObject CreateMonster(MonsterTypeId monsterType, Transform parent);
    GameObject CreateLoot(Vector3 transformPosition);
  }
}