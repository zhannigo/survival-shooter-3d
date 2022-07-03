using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
  public class ActorUI : MonoBehaviour
  {
    public HpBar HpBar;
    public HeroHealth HeroHealth;

    private void HealthUpdate()
    {
      HpBar.SetValue(HeroHealth.Current, HeroHealth.Max);
    }
  }
}