using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4.5f;
    public int bulletDamage = 5;
    private Vector3 direction;

    public void Initialize(Vector3 targetPosition)
    {
        // Calculate direction to player
        direction = (targetPosition - transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            Player_Health playerHealth = collision.gameObject.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        // Moves the bullet towards the calculated direction
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}


