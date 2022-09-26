using CodeBase.Data;
using CodeBase.Logic.MenuLogic;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class SelectButton : BuySkinService
  {
    public Button _button;

    public void Construct(PlayerProgress progress, CameraOnSelectedHero cameraOnHero) => 
      base.Construct(progress, cameraOnHero);

    protected override void CanSelect() => 
      _button.onClick.AddListener(SelectSkin);

    private void SelectSkin()
    {
      Selected = true;
      SaveProgress();
      SelectButtonOff();
    }

    protected override void SelectButtonOn() => 
      gameObject.SetActive(true);

    protected override void SelectButtonOff() => 
      gameObject.SetActive(false);
    
  }
}

