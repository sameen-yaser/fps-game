using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 1);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.StartsWith("enemy"))
        {
            Destroy(transform.gameObject);
        }
        
    }
}
