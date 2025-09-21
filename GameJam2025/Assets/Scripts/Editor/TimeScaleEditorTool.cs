using UnityEngine;
using UnityEditor;

public class TimeScaleEditorTool : EditorWindow
{
    [MenuItem("Tools/Time Scale Tool")]
    public static void ShowWindow()
    {
        GetWindow<TimeScaleEditorTool>("Time Scale Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Controle de Time.timeScale", EditorStyles.boldLabel);

        float newScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, 0f, 5f);

        if (!Mathf.Approximately(newScale, Time.timeScale))
        {
            Time.timeScale = newScale;
        }

        EditorGUILayout.LabelField("Atual: " + Time.timeScale.ToString("F2"));
    }
}
