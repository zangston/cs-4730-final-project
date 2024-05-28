using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_Movement : MonoBehaviour
{
    public float speed = 20.0f;
    public int plasmaDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            Player_Health playerHealth = collision.gameObject.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(plasmaDamage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

    void OnBecameInvisible()
    {
        // Debug.Log("DELETING LASER");
        Destroy(gameObject);
    }
}
