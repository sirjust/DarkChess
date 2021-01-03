using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(shadow))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        shadow myScript = (shadow)target;
        if (GUILayout.Button("Checker On"))
        {
            myScript.TurnShadowOff();
        }
        if (GUILayout.Button("Checker Off"))
        {
            myScript.TurnShadowOn();
        }
    }
}