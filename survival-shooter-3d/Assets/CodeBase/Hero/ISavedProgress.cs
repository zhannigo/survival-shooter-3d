using CodeBase.Data;

namespace CodeBase.Hero
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