using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using TMPro;

namespace CodeBase.UI.Forms.Shop
{
  public class ShopWindow : WindowBase
  {
    public TMP_Text LootCoinText;
    public RevardedShopItem ShopItem; 
    private IAdsService _adsService;

    public void Construct(IPersistentProgressService persistentProgressService, IAdsService adsService)
    {
      _adsService = adsService;
      base.Construct(persistentProgressService);
    }
    protected override void InitializeWindow()
    {
      ShopItem.Construct(_adsService, Progress);
      ShopItem.Initialize();
      RefreshLootText();
    }

    protected override void SubscribeUpdate()
    {
      ShopItem.Subscribe();
      Progress.WorldData.LootData.ChangedCollected += RefreshLootText;
    }

    protected override void Cleanup()
    {
      ShopItem.Cleanup();
      Progress.WorldData.LootData.ChangedCollected -= RefreshLootText;
    }

    private void RefreshLootText() => 
      LootCoinText.text = Progress.WorldData.LootData.Collected.ToString();
  }
}