using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
  [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window Static Data")]
  public class WindowsStaticData : ScriptableObject
  {
    public List<WindowsConfig> Configs;
  }
}