using System;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
  public class HeroMove : MonoBehaviour, ISavedProgress
  {
    public float MovementSpeed = 10f;
    
    public CharacterController _characterController;
    private IInputService _inputService;
    private Camera _camera;

    private void Awake()
    {
      _camera = Camera.main;
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;
      if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector.y = 0;
        movementVector = _camera.transform.TransformDirection(_inputService.Axis);
        movementVector.Normalize();
        transform.forward = movementVector;
        transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

      }
      movementVector += Physics.gravity;
      _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
      
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),transform.position.AsVector3Data());
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
      {
        Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
        if (savedPosition != null)
        {
          Warp(savedPosition);
        }
      }
    }

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    private static string CurrentLevel() => 
      SceneManager.GetActiveScene().name;
  }
}