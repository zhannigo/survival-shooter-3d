using System.Collections.Generic;
using System.Threading.Tasks;
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
    Task<GameObject> HeroCreate(Vector3 at);
    Task<GameObject> CreateHud();
    Task CreateSpawner(Vector3 at, MonsterTypeId monsterType, string spawnerID);
    void Cleanup();
    Task<GameObject> CreateMonster(MonsterTypeId monsterType, Transform parent);
    Task<LootPiece> CreateLoot(Vector3 transformPosition);
    void WarmUp();
  }
}