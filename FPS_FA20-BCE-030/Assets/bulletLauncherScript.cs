using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLauncherScript : MonoBehaviour
{
    public GameObject bullet;
    public AudioSource bulletaudio;
    // Start is called before the first frame update
    void Start()
    {
        bulletaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);
            bulletaudio.Play();
        }
        
    }
}