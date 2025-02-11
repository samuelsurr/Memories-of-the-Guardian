using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : Enemy //this will connect the code to enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target; // Transform means position, rotation, scale
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition; // Home position
    public Animator anim;

    // New variable for sprite renderer
    private SpriteRenderer spriteRenderer;

    // Variables for special move
    public float chargeSpeed;
    public float chargeDuration;
    public float chargeCooldown;
    private bool isCharging = false;
    private Vector3 chargeDirection;
    private float chargeEndTime;
    private float nextChargeTime;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>(); // Referencing to rigid body
        target = GameObject.FindWithTag("Player").transform; // Transform holds the location information
        // However, we set TARGET to equal the PLAYER'S POSITION

        // Initialize sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isCharging)
        {
            HandleCharging();
        }
        else if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            // Start charging if cooldown has passed
            if (Time.time >= nextChargeTime)
            {
                StartCharging();
            }
            else
            {
                // Normal chasing behavior
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);

                // New code to flip the sprite based on the player's position
                if (target.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = true; // Face left
                }
                else
                {
                    spriteRenderer.flipX = false; // Face right
                }
            }
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeDirection = (target.position - transform.position).normalized;
        chargeEndTime = Time.time + chargeDuration;
    }

    void HandleCharging()
    {
        if (Time.time < chargeEndTime)
        {
            Vector3 temp = transform.position + chargeDirection * chargeSpeed * Time.deltaTime;
            myRigidbody.MovePosition(temp);
        }
        else
        {
            isCharging = false;
            nextChargeTime = Time.time + chargeCooldown;
        }
    }
}
