using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
    }

    void Update()
    {
        transform.LookAt(fps.transform);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("bullet"))
        {
            print("hittt");
            anim.SetBool("isDead", true);
        }
        if (col.gameObject.name.StartsWith("grenade"))
        {
            print("hittt");
            anim.SetBool("isDead", true);
        }

    }
}
