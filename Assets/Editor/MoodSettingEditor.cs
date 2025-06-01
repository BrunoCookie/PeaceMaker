using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoodSetting))]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MoodSetting moodSetting = (MoodSetting)target;

        if (GUILayout.Button("Adjust the Gradient"))
        {
            moodSetting.moodEnd[moodSetting.moodEnd.Length - 1] = moodSetting.length;
            moodSetting.AdjustGradient();
            AssetDatabase.Refresh();
        }

        
    }
}
