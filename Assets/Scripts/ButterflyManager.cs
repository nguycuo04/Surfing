using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButterflyManager : MonoBehaviour
{
    public GameObject[] butterflyPrefabs; // Array to hold multiple prefabs
    public int numberOfButterflies = 20;
    public List<ButterflyBoid> butterflies = new List<ButterflyBoid>();
    public Vector2 spawnAreaSize = new Vector2(20, 20);
    public Vector2 areaCenter = Vector2.zero;
    public Vector2 areaSize = new Vector2(20, 20);
    public float minSpawnDistance = 1.5f; // Minimum distance between spawned prefabs
    [SerializeField] Transform player; 

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("BoidCenter"); // Ensure the player has the "Player" tag
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        SpawnButterflies();
    }
    private void Update()
    {
        if (player != null)
        {
            // Update the area center to the player's current position
            areaCenter = player.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(areaCenter, spawnAreaSize);
    }

    void SpawnButterflies()
    {
        for (int i = 0; i < numberOfButterflies; i++)
        {
            Vector2 spawnPosition;

            // Attempt to find a valid spawn position
            int attempts = 0;
            do
            {
                spawnPosition = new Vector2(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
                ) + areaCenter;

                attempts++;
                if (attempts > 50) // Prevent infinite loops
                {
                    Debug.LogWarning("Could not find a non-overlapping position for butterfly.");
                    break;
                }
            } while (IsOverlapping(spawnPosition));

            // Randomly select a prefab from the array
            GameObject randomPrefab = butterflyPrefabs[Random.Range(0, butterflyPrefabs.Length)];
            GameObject butterfly = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

            ButterflyBoid boid = butterfly.GetComponent<ButterflyBoid>();
            if (boid != null)
            {
                butterflies.Add(boid);
            }
            else
            {
                Debug.LogWarning("One of the butterfly prefabs is missing the ButterflyBoid component.");
            }
        }
    }

    bool IsOverlapping(Vector2 position)
    {
        foreach (ButterflyBoid butterfly in butterflies)
        {
            if (Vector2.Distance(position, butterfly.transform.position) < minSpawnDistance)
            {
                return true; // Overlapping
            }
        }
        return false; // Not overlapping
    }
}


