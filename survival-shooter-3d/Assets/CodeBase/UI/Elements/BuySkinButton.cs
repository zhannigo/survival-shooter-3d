using CodeBase.Data;
using CodeBase.Logic.MenuLogic;
using TMPro;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class BuySkinButton : BuySkinService
  {
    public Button _button;
    public TextMeshProUGUI _textPrice;
    private int _price;

    public void Construct(PlayerProgress progress, CameraOnSelectedHero cameraOnHero) => 
      base.Construct(progress, cameraOnHero);

    protected override void CanBuy()
    {
      UpdatePrice();
      _button.onClick.AddListener(BuySkin);
    }

    private void BuySkin()
    {
      LootData loot = Progress.WorldData.LootData;
      if(loot.Collected < _price || Buing)
      {
        return;
      }
      
      loot.Subtract(_price);
      Buing = true;
      SaveProgress();
      BuyButtonOff();
    }
    
    private void UpdatePrice()
    {
      _price = GetSkinPrice(SkinName);
      _textPrice.text = $"{_price}";
    }
    private int GetSkinPrice (string skinName)
    {
      SkinsPrice.TryGetValue(skinName, out int price);
      return price;
    }

    protected override void BuyButtonOff() => 
      gameObject.SetActive(false);

    protected override void BuyButtonOn() => 
      gameObject.SetActive(true);
    
  }
}

