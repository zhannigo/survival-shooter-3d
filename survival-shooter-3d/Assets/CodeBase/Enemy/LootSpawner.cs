using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class LootSpawner: MonoBehaviour
  {
    private IRandomService _random;
    private IGameFactory _gameFactory;
    private int _minValue;
    private int _maxValue;

    public void Construct(IGameFactory gameFactory, IRandomService randomService)
    {
      _gameFactory = gameFactory;
      _random = randomService;
    }
    private void Start() => 
      EnemyDeath.EnemyDead += SpawnLoot;

    private async void SpawnLoot()
    {
      LootPiece loot = await _gameFactory.CreateLoot(transform.position);
      Loot lootData = GenerateLoot();
      loot.Initialize(lootData);
    }

    private Loot GenerateLoot()
    {
      return new Loot()
      {
        Value = _random.Next(_minValue, _maxValue)
      };
    }

    public void SetLoot(int min, int max)
    {
      _minValue = min;
      _maxValue = max;
    }

    private EnemyDeath EnemyDeath => 
      GetComponentInParent<EnemyDeath>();
  }
}