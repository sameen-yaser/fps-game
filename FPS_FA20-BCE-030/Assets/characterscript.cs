using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class characterscript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            anim.SetTrigger("walk");
            transform.Translate(0, 0, 0.5f);
        }
        else
        {
            anim.SetTrigger("idle");
        }

        if (Input.GetKey(KeyCode.J))
        {
            anim.SetTrigger("jump");
        }
        else
        {
            anim.SetTrigger("idle");
        }
    }
}
