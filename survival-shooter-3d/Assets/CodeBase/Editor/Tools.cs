using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
