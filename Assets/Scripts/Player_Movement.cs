using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float dashMultiplier = 200.0f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float invincDuration = 0.2f;
    [SerializeField] private float maxStamina = 100f;  // Maximum stamina
    [SerializeField] private float staminaRecoveryRate = 50f;  // Rate at which stamina regenerates per second
    //[SerializeField] private float staminaCost = 50f;          // Stamina cost per dash
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject dashEffectPrefab;


    private Vector2 moveDirection;
    private float lastDashTime = -100f; 
    private bool isInvincible = false;
    private float currentStamina;

    // Make stamina variables public for StaminaBar code
    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina; 
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        RegenerateStamina();
  
        // Need half stamina bar to dash
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && currentStamina >= maxStamina * 0.5f)
        {
            //Debug.Log("Spacebar was pressed");
            StartCoroutine(Dash());
        }
    }
    void FixedUpdate()
    {
        // Regular movement is applied in FixedUpdate for consistent application of physics
        if (!IsDashing()) // Check if not dashing to apply regular movement
        {
            Move();
        }
    }
    private void ProcessInputs()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(dirX, dirY).normalized;

        // Check if player is moving
        if (moveDirection != Vector2.zero)
        {
            // Get direction angle in radians
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // Transform sprite
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        }
    }
    private void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private IEnumerator Dash()
    {
        lastDashTime = Time.time; // Update the last dash time to start cooldown
        isInvincible = true;
        Vector2 dashVelocity = (moveDirection * dashMultiplier) * 2.5f;
        // Reduce stamina when dashing, can dash twice in a row maximum
        currentStamina -= maxStamina*0.5f; 

        // Get angle of player movement
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        // Offset dash animation to be behind player
        Vector3 offset = -moveDirection.normalized * 1.0f; 

        // Instantiate DashEffect
        GameObject dashEffectInstance = Instantiate(dashEffectPrefab, transform.position + offset, Quaternion.Euler(0, 0, angle +180));
        // Follow player as it dashes
        dashEffectInstance.transform.parent = this.transform;


        
        // Apply dash velocity
        rb.velocity = dashVelocity;
        yield return new WaitForSeconds(dashDuration); // Wait for the dash to complete

       
        yield return new WaitForSeconds(invincDuration);
        isInvincible = false; // Turn off invincibility

    }

    private bool IsDashing()
    {
        return Time.time < lastDashTime + dashDuration;
    }
    public bool InvincibleCheck()
    {
        return isInvincible;
    }
    // Regenerate stamina
    void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
    }

}
