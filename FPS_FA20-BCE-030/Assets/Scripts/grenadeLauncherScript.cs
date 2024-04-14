using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeLauncherScript : MonoBehaviour
{
    public GameObject grenade;
    public AudioSource grenadeaudio;
    // Start is called before the first frame update
    void Start()
    {
        grenadeaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(grenade, transform.position, transform.rotation);
            grenadeaudio.Play();
        }
        
    }
}