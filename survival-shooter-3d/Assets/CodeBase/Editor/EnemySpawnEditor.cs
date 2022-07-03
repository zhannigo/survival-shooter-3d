using System.ComponentModel;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(EnemySpawner))]
  public class EnemySpawnEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(EnemySpawner enemySpawner, GizmoType gizmoType)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(enemySpawner.transform.position, 0.5f);
    }
  }
}