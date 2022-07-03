using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class RotateToHero:MonoBehaviour
  {
    private Transform _heroTransform;
    private IGameFactory _gameFactory;
    private Vector3 _positionToLook;
    public float _speed;

    private void Start()
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();
      if (HeroExist())
        InitializeHeroTransform();
      else
      {
        _gameFactory.HeroCreated += InitializeHeroTransform;
      }
    }

    private void Update()
    {
      if (Initialized())
      {
        UpdatePositionToLookAt();
        transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
      }
    }

    private bool HeroExist() => 
      _gameFactory.HeroGameObject != null;

    private void InitializeHeroTransform() => 
      _heroTransform = _gameFactory.HeroGameObject.transform;

    private bool Initialized()
    {
      return _heroTransform != null;
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDiff = _heroTransform.position - transform.position;
      _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
    }

    private Quaternion SmoothedRotation(Quaternion transform, Vector3 positionToLook)
    {
      return Quaternion.Lerp(transform, TargetRotation(positionToLook), SpeedFactor());
    }

    private static Quaternion TargetRotation(Vector3 positionToLook) => 
      Quaternion.LookRotation(positionToLook);

    private float SpeedFactor() => 
      _speed*Time.deltaTime;
  }
}