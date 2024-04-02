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
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.StartsWith("bullet"))
        {
            anim.SetTrigger("dead");
        }
        
    }
}
