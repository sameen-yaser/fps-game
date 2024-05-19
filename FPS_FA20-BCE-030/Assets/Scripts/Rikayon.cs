using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rikayon : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    public float detectionRadius = 10f; // Set detection radius to 10 units
    public float moveSpeed = 2f; // Speed at which the enemy moves
    public float roarDuration = 2f; // Duration of the roar in seconds
    public AudioSource roarAudio; // Reference to the roar audio source
    public Slider healthBar; // Reference to the health bar UI element

    private bool isRoaring = false; // Flag to check if the enemy is roaring
    private bool hasRoared = false; // Flag to check if the enemy has already roared
    private bool isAlive = true; // Flag to check if the enemy is alive

    public float maxHealth = 50f; // Maximum health of the boss
    private float currentHealth; // Current health of the boss

    void Start()
    {
        anim = GetComponent<Animator>();
        roarAudio = GetComponent<AudioSource>();
        currentHealth = maxHealth; // Initialize current health to max health
        healthBar.maxValue = maxHealth; // Set the max value of the health bar
        healthBar.value = currentHealth; // Set the initial value of the health bar
        healthBar.gameObject.SetActive(false); // Hide the health bar initially
    }

    void Update()
    {
        if (!isAlive) return; // If the enemy is dead, stop updating

        float distanceToPlayer = Vector3.Distance(transform.position, fps.transform.position);

        if (distanceToPlayer <= detectionRadius && !isRoaring && !hasRoared)
        {
            // Switch to roar animation when player enters the detection radius
            Debug.Log("Player detected");
            transform.LookAt(fps.transform);
            anim.SetTrigger("Roar");

            // Play roar audio once
            if (!roarAudio.isPlaying)
            {
                roarAudio.Play();
                Debug.Log("Playing roar audio");
            }

            hasRoared = true; // Set the flag to true to indicate that the enemy has roared
            // Start the roar coroutine
            StartCoroutine(RoarThenAttack());
        }
    }

    private IEnumerator RoarThenAttack()
    {
        isRoaring = true;

        healthBar.gameObject.SetActive(true); // Show the health bar when roaring

        // Wait for the roar duration before attacking
        yield return new WaitForSeconds(roarDuration);

        // Move the enemy towards the player if it is alive
        if (isAlive)
        {
            Debug.Log("boss Moving");
            anim.SetTrigger("Attack");

            while (isAlive && Vector3.Distance(transform.position, fps.transform.position) > 1f) // Ensure the enemy stops when close enough to the player or when dead
            {
                transform.LookAt(fps.transform);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isRoaring = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("bullet"))
        {
            // Reduce health by 10 when hit by a bullet
            currentHealth -= 10;
            healthBar.value = currentHealth; // Update the health bar
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Boss died!");
        isAlive = false;
        anim.SetTrigger("Die");
        healthBar.gameObject.SetActive(false); // Hide the health bar when the boss dies
        // Add any additional death effects here
    }
}
