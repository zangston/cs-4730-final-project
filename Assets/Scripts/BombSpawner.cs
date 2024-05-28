using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab; // Assign bomb prefab
    public float spawnInterval = 10.0f; // Time between spawns
    private GameObject player;

    // Position limits for random generation so that plasma bolts are spawned within level limits
    [SerializeField] private float minX = -9.0f;
    [SerializeField] private float maxX = 9.0f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float maxY = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        spawnInterval = 1.0f;
        InvokeRepeating("SpawnBombRandom", 2.0f, spawnInterval);
        //InvokeRepeating("SpawnBombAtPlayer", 2.0f, spawnInterval);
    }

    // Given X and Y positioning relative to spawner location (0, 0), spawn a plasma bolt with a given direction
    void SpawnBomb(float spawnX, float spawnY, float spawnRotation)
    {
        // Set spawn position of plasma 
        Vector3 spawnPosition = new Vector3(transform.position.x + spawnX, transform.position.y + spawnY, transform.position.z);

        // Instantiate new plasma, set rotation
        Instantiate(bombPrefab, spawnPosition, Quaternion.Euler(0, 0, spawnRotation));
        
    }

    // Spawn one plasma bolt at a random edge of the level in a random direction
    void SpawnBombRandom()
    {
        // Orientation variables for plasma spawn
        float spawnX = 0.0f;
        float spawnY = 0.0f;
        float spawnRotation = 0.0f;

        // Randomly generate flight direction of plasma
        // Generate a random integer between 0 and 3
        // 0: Up; 1: Right; 2: Down; 3: Left;
        int direction = UnityEngine.Random.Range(0, 4);

        // Use direction to determine the direction
        // General scheme: set X or Y random based on horizontal/vertical motion, then set other axis to spawn off edge of screen
        switch (direction)
        {
            // Perform actions for plasma shooting up
            case 0:
                // Debug.Log("Plasma direction: Up");

                spawnX = UnityEngine.Random.Range(minX, maxX);
                spawnY = -7.0f;
                spawnRotation = 90.0f;

                break;

            // Perform actions for laser shooting right
            case 1:
                // Debug.Log("Plasma direction: Right");

                spawnX = -12.0f;
                spawnY = UnityEngine.Random.Range(minY, maxY);
                spawnRotation = 0.0f;

                break;

            // Perform actions for plasma shooting down
            case 2:
                // Debug.Log("Plasma direction: Down");

                spawnX = UnityEngine.Random.Range(minX, maxX);
                spawnY = 7.0f;
                spawnRotation = -90.0f;

                break;

            // Perform actions for plasma shooting left
            case 3:
                // Debug.Log("Plasma direction: Left");

                spawnX = 12.0f;
                spawnY = UnityEngine.Random.Range(minY, maxY);
                spawnRotation = -180.0f;

                break;

            default:
                Debug.LogError("Invalid direction generated");
                break;
        }

        SpawnBomb(spawnX, spawnY, spawnRotation);
    }

    // Spawn plasma heading towards the player's coordinates from a random direction
    void SpawnBombAtPlayer()
    {
        // Orientation variables for plasma spawn
        float spawnX = 0.0f;
        float spawnY = 0.0f;
        float spawnRotation = 0.0f;

        // Randomly generate flight direction of plasma
        // Generate a random integer between 0 and 3
        // 0: Up; 1: Right; 2: Down; 3: Left;
        int direction = UnityEngine.Random.Range(0, 4);

        // Use direction to determine the direction
        // General scheme: set X or Y random based on horizontal/vertical motion, then set other axis to spawn off edge of screen
        switch (direction)
        {
            // Perform actions for plasma shooting up
            case 0:
                // Debug.Log("Plasma direction: Up");

                spawnX = player.transform.position.x;
                spawnY = -7.0f;
                spawnRotation = 90.0f;

                break;

            // Perform actions for laser shooting right
            case 1:
                // Debug.Log("Plasma direction: Right");

                spawnX = -12.0f;
                spawnY = player.transform.position.y;
                spawnRotation = 0.0f;

                break;

            // Perform actions for plasma shooting down
            case 2:
                // Debug.Log("Plasma direction: Down");

                spawnX = player.transform.position.x;
                spawnY = 7.0f;
                spawnRotation = -90.0f;

                break;

            // Perform actions for plasma shooting left
            case 3:
                // Debug.Log("Plasma direction: Left");

                spawnX = 12.0f;
                spawnY = player.transform.position.y;
                spawnRotation = -180.0f;

                break;

            default:
                Debug.LogError("Invalid direction generated");
                break;
        }

        SpawnBomb(spawnX, spawnY, spawnRotation);
    }
}
