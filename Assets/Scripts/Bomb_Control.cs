using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Control : MonoBehaviour
{
    public int bombDamage = 10;
    public float countdownDuration = 2.0f;

    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the hitbox collider at the start
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitbox.enabled = false;

        // Start the countdown when the bomb is spawned
        StartCoroutine(CountdownAndExplode());
    }

    // Coroutine to handle the countdown and explosion
    IEnumerator CountdownAndExplode()
    {
        // Wait for the countdown duration
        yield return new WaitForSeconds(countdownDuration);

        spriteRenderer.enabled = false;

        // Instantiate the explosion prefab
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Activate the hitbox
        ActivateHitbox();

        // Get the animation length of the explosion animation
        float explosionDuration = explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        // Wait for the duration of the explosion animation
        yield return new WaitForSeconds(explosionDuration * 0.8f);

        // Delete the explosion GameObject after playing the animation once
        Destroy(explosion);

        // Disable the bomb GameObject after the explosion
        gameObject.SetActive(false);
    }

    void ActivateHitbox()
    {
        hitbox.enabled = true;
    }

    // Deal damage to player if within damage radius (3x3 tiles)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            Player_Health playerHealth = collision.gameObject.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bombDamage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
