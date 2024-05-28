using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; 
    public float spawnInterval = 20.0f; 
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHeart", 15.0f, spawnInterval);   
    }

    private void Update()
    {
        // spawnTimer -= Time.deltaTime;
        // if (spawnTimer <= 0)
        // {
        //     SpawnHeart();
        //     spawnTimer = spawnInterval;
        // }
    }
    void SpawnHeart()
    {
        float x = Random.Range(-9.5f, 9.5f);
        float y = Random.Range(-3.15f, 3.15f);
        Vector3 spawnPosition = new Vector3(x, y, 0);
        Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
    }
}
