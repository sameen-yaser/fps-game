using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class grenadescript : MonoBehaviour
{
    public AudioSource grenadeaudio;
    void Start()
    {
        grenadeaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 1);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.StartsWith("enemy"))
        {
            grenadeaudio.Play();
        }

    }
}
