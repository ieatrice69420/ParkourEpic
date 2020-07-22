using UnityEngine;
using UnityEditor;
using DitzelGames.FastIK;

[CustomEditor(typeof(FastIKFabric))]
public class IKEditor : Editor
{
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FastIKFabric ik = (FastIKFabric)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Set Target Position"))
        {
            ik.SetTargetPos();
        }

        GUILayout.EndHorizontal();
    }
}
