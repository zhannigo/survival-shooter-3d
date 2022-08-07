using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services
{
  public interface IAdsService : IService
  {
    event Action RevardedVideoReady;
    bool IsRevardedVideoReady { get; }
    int Reward { get; }
    void Initialize();
    void ShowRevardedVideo(Action onVideoFinished);
  }
}