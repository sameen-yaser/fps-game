using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    public float detectionRadius = 30f;
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public AudioSource dieaudio;

    bool isRunning = true; // Flag to check if enemy is in running state

    void Start()
    {
        anim = GetComponent<Animator>();
        dieaudio = GetComponent<AudioSource>();
        anim.SetBool("isDead", false);
        anim.SetBool("isTrigger", false);
    }

    void Update()
    {
        if (!anim.GetBool("isDead") && !anim.GetBool("isTrigger"))
        {
            if (isRunning)
            {
                // Move the enemy forward
                transform.Translate(0, 0, 0.1f);
            }

            transform.LookAt(fps.transform);
            if (Vector3.Distance(transform.position, fps.transform.position) <= detectionRadius)
            {
                // Switch to shoot animation when player enters the radius 
                print("triggered");
                isRunning = false; // Stop running
                anim.SetBool("isTrigger", true);
            }
        }
    }

    //private void OnCollisionEnter(Collision col)
    //{
     //   if (col.gameObject.name.StartsWith("bullet") || col.gameObject.name.StartsWith("grenade"))
       // {
         //   print("hittt");
           // anim.SetBool("isDead", true);
            //dieaudio.Play();
        //}
    //}
    public void die()
    {
        anim.SetBool("isDead", true);
    }
}
