using System;
using CodeBase.UI.Forms;
using CodeBase.UI.Services;

namespace CodeBase.StaticData.Windows
{
  [Serializable]
  public class WindowsConfig
  {
    public WindowId WindowId;
    public WindowBase Prefab;
  }
}