namespace CodeBase.Infrastructure.Services
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}