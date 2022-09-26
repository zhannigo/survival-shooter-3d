using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.MenuLogic
{
  public class CameraPickHero : MonoBehaviour
  {
    public Button _buttonLeft;
    public Button _buttonRight;
    private CameraOnSelectedHero _cameraLogic;

    public void Construct(CameraOnSelectedHero cameraLogic) => 
      _cameraLogic = cameraLogic;

    private void Start()
    {
      _buttonRight.onClick.AddListener(PickHeroRight);
      _buttonLeft.onClick.AddListener(PickHeroLeft);
    }

    private void PickHeroLeft() => 
      _cameraLogic.PreviousTarget();

    private void PickHeroRight() => 
      _cameraLogic.NextTarget();
  }
}