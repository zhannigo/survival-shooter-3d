using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services;
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
    private IWindowService _windowService;
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    private GameObject HeroGameObject { get; set; }

    public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService progressService, IWindowService windowService)
    {
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
      _windowService = windowService;
    }

    public async void WarmUp()
    {
      await _assets.Load<GameObject>(AssetsAddress.LootPath);
      await _assets.Load<GameObject>(AssetsAddress.SpawnPath);
    }

    public async Task<GameObject> HeroCreate(Vector3 at)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.HeroPath);
      HeroGameObject = InstantiateRegistered(prefab, at);
      return HeroGameObject;
    }

    public async Task<GameObject> CreateHud()
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.HudPath);
      GameObject hud = InstantiateRegistered(prefab);
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.WorldData.LootData);
      foreach (OpenWindowButton windowButton in hud.GetComponentsInChildren<OpenWindowButton>())
      {
        windowButton.Construct(_windowService);
      }
      return hud;
    }

    public async Task CreateSpawner(Vector3 at, MonsterTypeId monsterType, string spawnerID)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.SpawnPath);
      SpawnPoint spawner = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();
      spawner.Construct(this);
      spawner.monsterType = monsterType;
      spawner.Id = spawnerID;
    }

    public async Task<GameObject> CreateMonster(MonsterTypeId monsterType, Transform parent)
    {
      MonsterStaticData enemyData = _staticData.ForMonster(monsterType);
      
      GameObject prefab = await _assets.Load<GameObject>(enemyData.Prefab);
      GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

      IHealth enemyHealth = monster.GetComponent<IHealth>();
      enemyHealth.CurrentHp = enemyData.Health;
      enemyHealth.MaxHp = enemyData.Health;

      Attack enemyAttack = monster.GetComponent<Attack>();
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

    public async Task<LootPiece> CreateLoot(Vector3 at)
    {
      GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.LootPath);
      LootPiece lootPiece = InstantiateRegistered(prefab, at)
        .GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.WorldData);
      return lootPiece;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
      _assets.Cleanup();
    }

    private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>()) {
        Register(progressReader);
      }
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }
      ProgressReaders.Add(progressReader);
    }
  }
}