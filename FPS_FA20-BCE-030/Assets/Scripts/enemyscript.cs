using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    public float detectionRadius = 10f; // Set detection radius to 10 units
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public AudioSource roarAudio; // Reference to the roar audio source
    private bool isRoaring = false; // Flag to check if the enemy is roaring
    private bool isAttacking = false; // Flag to check if the enemy is attacking
    private bool isDead = false; // Flag to check if the enemy is dead

    void Start()
    {
        anim = GetComponent<Animator>();
        roarAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isRoaring && !isAttacking && !isDead && Vector3.Distance(transform.position, fps.transform.position) <= detectionRadius)
        {
            // Switch to roar animation when player enters the detection radius
            print("Player detected");
            isRoaring = true;
            anim.SetTrigger("Roar");

            // Play roar audio once
            if (!roarAudio.isPlaying)
            {
                roarAudio.Play();
            }
        }

        if (isAttacking && !isDead)
        {
            // Move the enemy towards the player
            transform.LookAt(fps.transform);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    // This function will be called from the animation event at the end of the Roar animation
    public void OnRoarComplete()
    {
        if (!isDead)
        {
            isRoaring = false;
            isAttacking = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!isDead && (col.gameObject.name.StartsWith("bullet") || col.gameObject.name.StartsWith("grenade")))
        {
            print("hittt");
            isDead = true;
            isAttacking = false; // Stop attacking when dead
            anim.SetTrigger("Die");
        }
    }
}
