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
            myScript.TurnCheckerOff();
        }
        if (GUILayout.Button("Checker Off"))
        {
            myScript.TurnCheckerOn();
        }
        if (GUILayout.Button("Shadow On"))
        {
            myScript.TurnShadowOn();
        }
        if (GUILayout.Button("Shadow Off"))
        {
            myScript.TurnShadowOff();
        }
    }
}