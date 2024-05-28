using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turretPrefab;
    // Spawn interval
    public float spawnInterval = 20.0f;
    private float spawnTimer;
    // List to keep track of spawn locations
    private List<Vector3> spawnList = new List<Vector3>();
    private int maxTurrets = 6; // Maximum number of turrets that can be spawned
    private int currentTurretCount = 0; // Current number of spawned turrets

    void Start()
    {
        spawnTimer = spawnInterval;  // Initialize the timer to start at the spawn interval
    }


    void Update()
    {
        // Only spawn 6 turrets
        if (currentTurretCount < maxTurrets){
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Vector3 spawnPosition = FindValidSpawnPosition();
                // Ensure spawn positions don't overlap
                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
                    // Add to spawn list
                    spawnList.Add(spawnPosition);
                    spawnTimer = spawnInterval;
                    currentTurretCount++; // Increment the turret count

                }
            }
        }
    }
    // Find valid spawn locations
    Vector3 FindValidSpawnPosition()
    {
        for (int attempts = 0; attempts < 50; attempts++)
        {
            float xPosition;
            float yPosition = Random.Range(2.65f, 3.3f);
            // Don't spawn in the middle section of the game
            if (Random.value < 0.5f) 
            {
                // Spawn on the left side 
                xPosition = Random.Range(-8.3f, -5.5f);
            }
            else
            {
                // Spawn on the right side
                xPosition = Random.Range(5.5f, 8.3f);
            }
            
            Vector3 newSpawnPosition = new Vector3(xPosition, yPosition, 0);
            // Check if position is valid and not close to other turrets
            if (IsPositionValid(newSpawnPosition))
            {
                return newSpawnPosition;
            }
        }
        return Vector3.zero;
    }


    bool IsPositionValid(Vector3 newSpawnPosition)
    {
        foreach (Vector3 existingPosition in spawnList)
        {
            // Ensure turrets don't spawn within 1.5 units of each other
            if (Vector3.Distance(newSpawnPosition, existingPosition) < 1.5f)
            {
                return false; // If too close to an existing turret, position is invalid
            }
        }
        return true; // Position is valid if it is not too close to any existing turret
    }
}
