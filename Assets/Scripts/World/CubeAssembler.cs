using UnityEngine;

public class CubeAssembler : MonoBehaviour
{
    public GameObject cubePrefab; // Assign your cube prefab in the inspector
    public float cubeSize = 1f; // Size of the cube (assuming uniform size for all axes)

    void Start()
    {
        AssembleCubes();
    }

    void AssembleCubes()
    {
        // Define the positions and rotations for the 4 smaller cubes to form a larger cube
        Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(cubeSize, 0, 0),
            new Vector3(0, cubeSize, 0),
            new Vector3(cubeSize, cubeSize, 0)
        };

        Quaternion[] rotations = new Quaternion[]
        {
            Quaternion.identity,
            Quaternion.identity,
            Quaternion.identity,
            Quaternion.identity
        };

        // Instantiate the cubes at the specified positions and rotations
        for (int i = 0; i < positions.Length; i++)
        {
            Instantiate(cubePrefab, positions[i], rotations[i]);
        }
    }
}
