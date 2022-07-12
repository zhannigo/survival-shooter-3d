using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnPoint : MonoBehaviour, ISavedProgress
  {
    public MonsterTypeId monsterType;
    public bool Slain;
    public string Id { get; set; }
    private IGameFactory _factory;
    private EnemyDeath _enemyDeath;

    public void Construct(IGameFactory gameFactory) => 
      _factory = gameFactory;

    public void LoadProgress(PlayerProgress progress)
    {
      if (progress.KillData.ClearedSpawners.Contains(Id)) 
        Slain = true;
      else {
        Spawn();
      }
    }
    private void Spawn()
    {
      GameObject monster = _factory.CreateMonster(monsterType, transform);
      _enemyDeath = monster.GetComponent<EnemyDeath>();
      _enemyDeath.EnemyDead += Slay;
    }

    private void Slay()
    {
      if (!(_enemyDeath is null)) 
        _enemyDeath.EnemyDead -= Slay;
      Slain = true;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if(Slain) 
        progress.KillData.ClearedSpawners.Add(Id);
    }
  }
}