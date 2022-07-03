using System;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class LootSpawner: MonoBehaviour
  {
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    private void Start()
    {
      EnemyDeath.EnemyDead += SpawnLoot;
    }
    private void SpawnLoot()
    {
      _gameFactory.CreateLoot(transform.position);
    }

    private EnemyDeath EnemyDeath => 
      GetComponentInParent<EnemyDeath>();
  }
}