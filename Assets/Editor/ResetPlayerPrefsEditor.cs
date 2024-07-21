using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ResetPlayerPrefsEditor
{
    static ResetPlayerPrefsEditor()
    {
        EditorApplication.playModeStateChanged += ResetPlayerPrefsOnExitPlayMode;
    }

    private static void ResetPlayerPrefsOnExitPlayMode(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            // Clear specific PlayerPrefs entry for your scene
            PlayerPrefs.DeleteKey("MainGame");
        }
    }
}
