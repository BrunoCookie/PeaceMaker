using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Score))]
public class ScoreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Score score = (Score)target;
        if (GUILayout.Button("Reset the Score"))
        {
            score.ResetScore();
            AssetDatabase.Refresh();
        }

    }
}
