using CodeBase.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.SaveLoadService
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}