using CodeBase.Data;

namespace CodeBase.Infrastructure
{
  public interface ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }

  public interface ISavedProgress : ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);

  }
}