using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class SkinsData
  {
    public List<string> BuyingSkins = new List<string>();
    public string selectedHero;
  }
}

