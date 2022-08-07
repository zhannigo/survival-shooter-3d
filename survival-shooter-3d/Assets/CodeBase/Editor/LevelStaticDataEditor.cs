using System.Linq;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialpointTag = "InitialPoint";
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      LevelStaticData staticData = (LevelStaticData)target;
      if (GUILayout.Button("Collect"))
      {
        staticData.Spawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().id, x.monsterType, x.transform.position))
          .ToList();
        staticData.LevelKey = SceneManager.GetActiveScene().name;
        staticData.InitialPointPosition = GameObject.FindGameObjectWithTag(InitialpointTag).transform.position;
      }
      EditorUtility.SetDirty(target);
    }
  }
}