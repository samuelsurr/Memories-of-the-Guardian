using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy //this will connect the code to enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target; // Transform means position, rotation, scale
    public float chaseRadius;
    public float attackRadius;
    public Transform homePositon; // Home position
    public Animator anim;

    // New variable for sprite renderer
    private SpriteRenderer spriteRenderer;

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
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            // We need to look for distance between two things whenever we do Vector3.Distance
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
