using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor {

    private TerrainGenerator generator;

    public override void OnInspectorGUI() {

        if(GUILayout.Button("Generate")) {
            generator.Generate();
        }

        if(GUILayout.Button("Clear")) {
            generator.ClearTiles();
        }

        base.OnInspectorGUI();

        // using(var check = new EditorGUI.ChangeCheckScope()) {

        //     if(check.changed) {
        //         generator.Generate();
        //     }

        // }

    }

    private void OnEnable() {
        generator = (TerrainGenerator)target;
    }

}