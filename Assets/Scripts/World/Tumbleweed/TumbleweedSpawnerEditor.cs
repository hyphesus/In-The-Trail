#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(TumbleweedSpawner))]
public class TumbleweedSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TumbleweedSpawner spawner = (TumbleweedSpawner)target;

        spawner.tumbleweedPrefab = (GameObject)EditorGUILayout.ObjectField("Tumbleweed Prefab", spawner.tumbleweedPrefab, typeof(GameObject), false);
        spawner.areaCenter = EditorGUILayout.Vector3Field("Area Center", spawner.areaCenter);
        spawner.areaSize = EditorGUILayout.Vector3Field("Area Size", spawner.areaSize);
        spawner.tumbleweedCount = EditorGUILayout.IntSlider("Tumbleweed Count", spawner.tumbleweedCount, 1, 100);

        if (GUILayout.Button("Spawn Tumbleweeds"))
        {
            spawner.SpawnTumbleweeds();
        }

        DrawDefaultInspector();
    }
}
#endif
