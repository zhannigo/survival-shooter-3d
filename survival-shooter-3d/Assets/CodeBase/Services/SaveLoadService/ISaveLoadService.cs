using CodeBase.Data;

namespace CodeBase.Infrastructure.States
{
  public interface ISaveLoadService
  {
    PlayerProgress LoadProgress();
  }
}