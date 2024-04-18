using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunscript : MonoBehaviour
{
    public GameObject character;
    public float range = 500;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shoot();
            //gun recoil
            anim.SetTrigger("recoil");
        }
        else
        {
            anim.SetTrigger("idle");
        }
    }

    public void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, character.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "enemy")
            {
                //die animation
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>(); 
                enemy.die();
            }
        }
    }
}
