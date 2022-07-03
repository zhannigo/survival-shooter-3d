using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoadService
{
  public class SaveLoadService : ISaveLoadService
  {
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;
    private const string ProgressKey = "Progress";

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
      {
        progressWriter.UpdateProgress(_progressService.Progress);
      }
      PlayerPrefs.SetString(ProgressKey,_progressService.Progress.ToJson());
    }
    public PlayerProgress LoadProgress() => 
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
  }
}