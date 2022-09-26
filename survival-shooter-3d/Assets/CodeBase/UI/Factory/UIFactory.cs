using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManager;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Forms;
using CodeBase.UI.Forms.Shop;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.UI.Factory
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssets _assets;
    private IStaticDataService _staticData;
    private const string UIRootPath = "UIRoot";
    private WindowId _windowId;
    
    private Transform _uiRoot;
    private IPersistentProgressService _progressService;
    private IAdsService _adsService;

    public UIFactory (IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _adsService = adsService;
    }

    public void CreateShop()
    {
      WindowsConfig config = _staticData.ForWindow(WindowId.Shop);
      ShopWindow shopWindow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
      shopWindow.Construct(_progressService, _adsService);
    }

    public async Task CreateUIRoot()
    {
      var prefab = await _assets.Instanstiate(UIRootPath);
      _uiRoot = prefab.transform;
    }
  }
}