using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}