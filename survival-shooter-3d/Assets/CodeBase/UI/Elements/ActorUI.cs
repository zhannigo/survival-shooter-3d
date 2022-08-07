using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorUI : MonoBehaviour
  {
    public HpBar HpBar;
    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChanged += UpdateBar;
    }

    public void OnDestroy() => 
        _health.HealthChanged -= UpdateBar;

    private void UpdateBar()
    {
      HpBar.SetValue(_health.CurrentHp, _health.MaxHp);
    }
  }
}