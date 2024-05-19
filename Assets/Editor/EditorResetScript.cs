#if UNITY_EDITOR
using UnityEditor;
using UnityEngine; // Required to access PlayerPrefs

[InitializeOnLoad]
public class EditorResetScript
{
    static EditorResetScript()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.Save();
    }
}
#endif
