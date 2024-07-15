using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class VoxelWorldGenerator : MonoBehaviour
{
    public GameObject voxelPrefab;
    public int width = 10;
    public int length = 10;
    public float voxelSize = 1f;

    private bool worldGenerated = false;
    private bool needsRegeneration = false;

    void Start()
    {
        if (!Application.isPlaying && !worldGenerated)
        {
            GenerateWorld();
            worldGenerated = true;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Generate World In Editor")]
    public void GenerateWorldInEditor()
    {
        needsRegeneration = true;
        EditorApplication.delayCall += () => GenerateWorld();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying && !worldGenerated)
        {
            needsRegeneration = true;
        }
    }
#endif

    void Update()
    {
#if UNITY_EDITOR
        if (needsRegeneration)
        {
            RegenerateWorld();
            needsRegeneration = false;
        }
#endif
    }

    void RegenerateWorld()
    {
        // Clear existing children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        GenerateWorld();

#if UNITY_EDITOR
        // Mark the scene as dirty to save the changes
        EditorUtility.SetDirty(gameObject);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
    }

    void GenerateWorld()
    {
        if (voxelPrefab == null)
        {
            Debug.LogError("Voxel Prefab not assigned!");
            return;
        }

        // Generate new world
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                Vector3 position = new Vector3(x * voxelSize, 0, z * voxelSize);
                GameObject voxel = Instantiate(voxelPrefab, position, Quaternion.identity, transform);
                voxel.name = $"Voxel_{x}_{z}";
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(VoxelWorldGenerator))]
public class VoxelWorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        VoxelWorldGenerator generator = (VoxelWorldGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate World In Editor"))
        {
            generator.GenerateWorldInEditor();
        }
    }
}
#endif
