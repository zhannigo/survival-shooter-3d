using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "SkinsData", menuName = "StaticData/HeroSkins")]
  public class SkinsStaticData : ScriptableObject
  {
    public AssetReference Prefab;
    public SkinsTypePath _skinsType;
    public int _price;
    
    public SkinsTypePath SkinsType
    {
      get { return _skinsType; }
      protected set { _skinsType = value; }
    }
  }
}

