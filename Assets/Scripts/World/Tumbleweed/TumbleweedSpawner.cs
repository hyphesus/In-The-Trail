using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TumbleweedSpawner : MonoBehaviour
{
    public GameObject tumbleweedPrefab; // Assign the Tumbleweed prefab with TumbleweedAnimation script attached
    public Vector3 areaCenter; // Center of the spawning area
    public Vector3 areaSize; // Size of the spawning area
    public int tumbleweedCount = 3; // Number of tumbleweeds to spawn per interval
    public float destructionDelay = 5f; // Delay before destroying the object after the animation ends
    public float spawnInterval = 25f; // Interval time in seconds for spawning tumbleweeds

    private List<GameObject> spawnedTumbleweeds = new List<GameObject>();

    private void Start()
    {
        // Start the coroutine to spawn tumbleweeds at intervals
        StartCoroutine(SpawnTumbleweedsAtIntervals());
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the spawning area in the scene view for easier adjustment
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(areaCenter, areaSize);
    }

    private IEnumerator SpawnTumbleweedsAtIntervals()
    {
        while (true)
        {
            SpawnTumbleweeds();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnTumbleweeds()
    {
        // Clear existing tumbleweeds
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn new tumbleweeds
        for (int i = 0; i < tumbleweedCount; i++)
        {
            Vector3 randomPosition = GetRandomPositionInArea();
            GameObject tumbleweed = Instantiate(tumbleweedPrefab, randomPosition, Quaternion.identity, transform);
            TumbleweedAnimation animation = tumbleweed.GetComponent<TumbleweedAnimation>();
            if (animation != null)
            {
                animation.startPoint = randomPosition;
                animation.endPoint = randomPosition + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // Example end point
                animation.OnAnimationEnd += () => StartCoroutine(DestroyTumbleweedAfterDelay(tumbleweed, destructionDelay));
            }
            spawnedTumbleweeds.Add(tumbleweed);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        return new Vector3(randomX, areaCenter.y, randomZ);
    }

    private IEnumerator DestroyTumbleweedAfterDelay(GameObject tumbleweed, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnedTumbleweeds.Remove(tumbleweed);
        Destroy(tumbleweed);
    }
}
