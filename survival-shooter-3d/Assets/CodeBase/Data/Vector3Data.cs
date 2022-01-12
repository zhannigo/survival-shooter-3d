using System;

namespace CodeBase.Data
{
  [Serializable]
  public class Vector3Data
  {
    public static float X;
    public static float Y;
    public static float Z;

    public Vector3Data(float x, float y, float z)
    {
      X = x;
      Y = y;
      Z = z;
    }
  }
}