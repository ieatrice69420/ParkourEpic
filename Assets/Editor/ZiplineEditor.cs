using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Zipline))]
public class ZiplineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Zipline zipline = (Zipline)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Look At Start"))
        {
            zipline.LookAtStart();
        }

        if (GUILayout.Button("Look At End"))
        {
            zipline.LookAtEnd();
        }

        if (GUILayout.Button("Fix Carrier Rotation"))
        {
            zipline.FixCarrierRotation();
        }

        GUILayout.EndHorizontal();
    }
}