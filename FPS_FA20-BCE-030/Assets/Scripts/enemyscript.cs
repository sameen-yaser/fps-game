using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    public float detectionRadius = 10f; // Set detection radius to 10 units
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public float roarDuration = 2f; // Duration of the roar in seconds
    public AudioSource roarAudio; // Reference to the roar audio source
    private bool isRoaring = false; // Flag to check if the enemy is roaring
    private bool hasRoared = false; // Flag to check if the enemy has already roared
    private bool isAlive = true; // Flag to check if the enemy is alive

    void Start()
    {
        anim = GetComponent<Animator>();
        roarAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isAlive) return; // If the enemy is dead, stop updating

        float distanceToPlayer = Vector3.Distance(transform.position, fps.transform.position);
        Debug.Log($"Distance to player: {distanceToPlayer}");

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

        // Wait for the roar duration before attacking
        yield return new WaitForSeconds(roarDuration);

        // Move the enemy towards the player if it is alive
        if (isAlive)
        {
            Debug.Log("Moving towards the player");
            anim.SetTrigger("Attack");

            while (Vector3.Distance(transform.position, fps.transform.position) > 1f) // Ensure the enemy stops when close enough to the player
            {
                transform.LookAt(fps.transform);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isRoaring = false;
    }

    public void die()
    {
        Debug.Log("Enemy died");
        isAlive = false;
        anim.SetTrigger("Die");
    }
}
