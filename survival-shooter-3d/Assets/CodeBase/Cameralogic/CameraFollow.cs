using System;
using UnityEngine;

namespace CodeBase.Cameralogic
{
  public class CameraFollow : MonoBehaviour
  {
    public float RotationAngleX;
    public float Distance;
    public float OffsetY;

    [SerializeField] 
    public Transform _following;

    public void Follow(GameObject following) => 
      _following = following.transform;

    private void LateUpdate()
    {
      if (_following == null)
        return;

      Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
      Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
      transform.rotation = rotation;
      transform.position = position;
    }

    private Vector3 FollowingPointPosition()
    {
      Vector3 followingPosition = _following.position;
      followingPosition.y += OffsetY;
      return followingPosition;
    }
  }
}