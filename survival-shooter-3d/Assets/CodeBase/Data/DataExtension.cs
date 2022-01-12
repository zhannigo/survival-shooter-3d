using UnityEngine;

namespace CodeBase.Data
{
  public static class DataExtension
  {
    public static Vector3Data AsVector3Data(this Vector3 vector3) =>
      new Vector3Data(vector3.x, vector3.y, vector3.z);

    public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
      new Vector3(Vector3Data.X, Vector3Data.Y, Vector3Data.Z);

    public static T ToDeserialized<T>(this string json) => 
      JsonUtility.FromJson<T>(json);
    
    public static string ToJson(this object obj) => 
      JsonUtility.ToJson(obj);
  }
}