using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SentryTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootingInterval = 2.0f;
    private float shootingTimer;
    private Transform player;

    void Start()
    {
        // Find player
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            AimAndShoot();
        }
    }

    private void AimAndShoot()
    {
        // Aim towards the player
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Shooting logic
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
        shootingTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Create rotation towards the player
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Instantiate bullet with calculated rotation
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.GetComponent<Bullet>().Initialize(direction.normalized);
        }
    }



}

