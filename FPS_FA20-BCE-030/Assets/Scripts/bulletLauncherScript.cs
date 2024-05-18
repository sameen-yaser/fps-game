using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletLauncherScript : MonoBehaviour
{
    public GameObject bullet;
    public AudioSource bulletaudio;
    public Text bulletText;

    private int bulletCount = 20;

    // Start is called before the first frame update
    void Start()
    {
        bulletaudio = GetComponent<AudioSource>();
        UpdateBulletText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            bulletaudio.Play();
            bulletCount--;
            UpdateBulletText();
        }
    }

    void UpdateBulletText()
    {
        bulletText.text = "Bullets: " + bulletCount.ToString();
    }
}
