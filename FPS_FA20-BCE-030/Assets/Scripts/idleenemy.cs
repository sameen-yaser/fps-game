using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleenemy : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    public GameObject bulletPrefab;
    public float timeBetweenShots = 2f;
    public GameObject ammoCrate; // Reference to the ammo crate prefab
    public float detectionRadius = 60f;
    public float moveSpeed = 5f; // Speed at which the enemy moves

    private int currentBullets;
    private bool hasDroppedAmmo = false; // Flag to check if ammo has been dropped

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        anim.SetBool("isTrigger", false);
        currentBullets = 0;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (!anim.GetBool("isDead") && !anim.GetBool("isTrigger"))
        {
            transform.LookAt(fps.transform);

            if (Vector3.Distance(transform.position, fps.transform.position) <= detectionRadius)
            {
                // Switch to shoot animation when player enters the radius 
                print("triggered");
                anim.SetBool("isTrigger", true);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!hasDroppedAmmo && (col.gameObject.name.StartsWith("bullet") || col.gameObject.name.StartsWith("grenade")))
        {
            Instantiate(ammoCrate, transform.position, Quaternion.identity);
            print("hittt");
            anim.SetBool("isDead", true);
            hasDroppedAmmo = true;
        }
    }

    IEnumerator Shoot()
    {
        while (!anim.GetBool("isDead"))
        {
            // Check if the enemy is in shooting position (IsTrigger is true)
            if (anim.GetBool("isTrigger"))
            {
                InstantiateBullet();
                yield return new WaitForSeconds(timeBetweenShots);
            }
            else
            {
                yield return null; // Wait until the enemy is in shooting position
            }
        }
    }

    void InstantiateBullet()
    {
        if (currentBullets < 10)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, 6.86f, transform.position.z); // Set y-coordinate to 6.86
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            currentBullets++;
        }
    }
}
