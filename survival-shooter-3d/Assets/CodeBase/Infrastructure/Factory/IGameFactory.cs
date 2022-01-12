using UnityEngine;

namespace CodeBase.Infrastructure
{
  public interface IGameFactory
  {
    GameObject HeroCreate(GameObject at);
    void CreateHub();
  }
}