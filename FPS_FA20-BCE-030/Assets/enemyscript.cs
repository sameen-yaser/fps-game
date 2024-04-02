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

    void Update()
    {
        transform.Translate(0, 0, 0.1f);
        transform.LookAt(fps.transform);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.StartsWith("bullet"))
        {
            anim.SetTrigger("dead");
        }
        
    }
}
