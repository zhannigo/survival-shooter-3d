using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
  public class MonsterStaticData : ScriptableObject
  {
    public MonsterTypeId MonsterType;
    [Range(1, 50)] public int Health;
    [Range(1, 10)] public float MoveSpeed = 4f;
    [Range(1, 30)] public float Damage = 10f;
    [Range(1, 30)] public float AttackCooldown = 1f;
    [Range(1, 30)] public float Cleavage = 0.5f;
    [Range(1, 30)] public float EffectiveDistance=0.5f;
    public GameObject Prefab;
  }
}