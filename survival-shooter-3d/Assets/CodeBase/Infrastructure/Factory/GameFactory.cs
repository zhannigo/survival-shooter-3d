using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private IPersistentProgressService _progressService;
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    private GameObject HeroGameObject { get; set; }

    public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService progressService)
    {
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
    }

    public GameObject HeroCreate(GameObject at)
    {
      HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
      return HeroGameObject;
    }

    public GameObject CreateHud()
    {
      GameObject hud = InstantiateRegistered(AssetPath.HudPath);
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.WorldData.LootData);
      return hud;
    }

    public void CreateSpawner(Vector3 at, MonsterTypeId monsterType, string spawnerID)
    {
      var spawner = InstantiateRegistered(AssetPath.SpawnPath, at).GetComponent<SpawnPoint>();
      spawner.Construct(this);
      spawner.monsterType = monsterType;
      spawner.Id = spawnerID;
    }

    public GameObject CreateMonster(MonsterTypeId monsterType, Transform parent)
    {
      MonsterStaticData enemyData = _staticData.ForMonster(monsterType);
      GameObject monster = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

      var enemyHealth = monster.GetComponent<IHealth>();
      enemyHealth.CurrentHp = enemyData.Health;
      enemyHealth.MaxHp = enemyData.Health;

      var enemyAttack = monster.GetComponent<Attack>();
      enemyAttack.Construct(HeroGameObject.transform);
      enemyAttack.Cleavage = enemyData.Cleavage;
      enemyAttack.AttackCooldown = enemyData.AttackCooldown;
      enemyAttack.EffectiveDistance = enemyData.EffectiveDistance;
      enemyAttack.Damage = enemyData.Damage;

      monster.GetComponent<ActorUI>().Construct(enemyHealth);
      monster.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
      monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
      monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

      LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetLoot(enemyData.minValue, enemyData.maxValue);
      lootSpawner.Construct(this, _randomService);

      return monster;
    }

    public LootPiece CreateLoot(Vector3 position)
    {
      LootPiece lootPiece = InstantiateRegistered(AssetPath.LootPath, position)
        .GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.WorldData);
      return lootPiece;
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instanstiate(prefabPath, at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instanstiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>()) {
        Register(progressReader);
      }
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }
      ProgressReaders.Add(progressReader);
    }
  }
}