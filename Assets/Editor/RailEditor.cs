using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rail))]
public class RailEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Rail rail = (Rail)target;
        if (GUILayout.Button("Set Children as Rail"))
        {
            rail.nodes = new Transform[rail.transform.childCount];
            for (int i = 0; i < rail.transform.childCount; i++)
            {
                rail.nodes[i] = rail.transform.GetChild(i);
            }
            AssetDatabase.Refresh();
        }

    }
}
