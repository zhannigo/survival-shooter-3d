using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.MenuLogic
{
  public class CameraOnSelectedHero : MonoBehaviour
  {
    public float _distance = 1f;
    public float _height = 1;
    public float _lookAtAroundAngle = 180;
    
    public List<Transform> TargetsTransform;
    public Dictionary<string, Transform> Targets;
    public Dictionary<string, int> SkinsPrice;

    public string SelectedHeroName { get; set; }

    public event Action SkinChanged;
    
    private Transform _currentTransform;
    private int _currentIndex;

    public void Initialize(string name)
    {
      SelectedHeroName = name;
      _currentTransform = GetSkinTransform(name);
      _currentIndex = TargetsTransform.IndexOf(_currentTransform);
    }

    private void SwitchTarget(int step)
    {
      if (TargetsTransform.Count == 0)
        return;
      
      _currentIndex += step;
      if (_currentIndex > TargetsTransform.Count - 1) { _currentIndex = 0; }
      if (_currentIndex < 0) { _currentIndex = TargetsTransform.Count - 1; }
      _currentTransform = TargetsTransform[_currentIndex];
      
      SelectedHeroName = Targets.FirstOrDefault(x => x.Value == _currentTransform).Key;
      SkinChanged?.Invoke();
    }

    public void NextTarget()
    {
      if (TargetsTransform.Count == 0)
        return;
      
      SwitchTarget(1);
    }

    public void PreviousTarget()
    {
      if (TargetsTransform.Count == 0)
        return;
      
      SwitchTarget(-1);
    }

    private void LateUpdate()
    {
      if (_currentTransform == null) 
        return;

      float targetHeight = _currentTransform.position.y + _height;
      float currentRotationAngle = _lookAtAroundAngle;
      Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

      Vector3 position = _currentTransform.position;
      position -= currentRotation * Vector3.forward * _distance;
      position.y = targetHeight;

      transform.position = position;
      transform.LookAt(_currentTransform.position + new Vector3(0, _height, 0));
    }

    private Transform GetSkinTransform (string heroName)
    {
      return Targets.TryGetValue(heroName, out Transform heroTransform)
        ? heroTransform
        : null;
    }
  }
}

