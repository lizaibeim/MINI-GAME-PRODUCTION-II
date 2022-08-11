using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MagneticValueAssigner))]
public class MagneticValueAssignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MagneticValueAssigner assigner = (MagneticValueAssigner)target;

        if (GUILayout.Button("Find scripts"))
        {
            assigner.FindMagneticScripts();
        }

        if (GUILayout.Button("Assign values"))
        {
            assigner.AssignMagnetValues();
        }
    }
}
