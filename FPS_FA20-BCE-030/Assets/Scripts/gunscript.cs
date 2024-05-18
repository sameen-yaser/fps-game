using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gunscript : MonoBehaviour
{
    public GameObject fpsController;

    public Slider HealthBar;
    public Text bulletCountText;
    public Text grenadeCountText;
    public GameObject bullet;
    public GameObject grenade;
    public AudioSource bulletaudio;

    private float maxHealth = 50;
    private float health;
    private int bulletCount = 20;
    private int grenadeCount = 2;

    void Awake()
    {
        health = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;

        UpdateBulletCountText();
        UpdateGrenadeCountText();

        bulletaudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0)
        {
            LaunchGrenade();
        }
    }

    void ShootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        bulletaudio.Play();
        bulletCount--;
        UpdateBulletCountText();
    }

    void LaunchGrenade()
    {
        Instantiate(grenade, transform.position, transform.rotation);
        grenadeCount--;
        UpdateGrenadeCountText();
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            print("Player hit");
            health -= 1;
            HealthBar.value = health;

            if (health <= 0)
            {
                Debug.Log("Player died!");
            }

        }

        if (col.gameObject.tag.StartsWith("med"))
        {
            health += 10;
            if (health > maxHealth)
                health = maxHealth;
            HealthBar.value = health;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag.StartsWith("ammo"))
        {
            bulletCount = 20;
            UpdateBulletCountText();
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag.StartsWith("grenade"))
        {
            grenadeCount++;
            UpdateGrenadeCountText();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag.StartsWith("win"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateBulletCountText()
    {
        bulletCountText.text = "Bullets: " + bulletCount.ToString();
    }

    void UpdateGrenadeCountText()
    {
        grenadeCountText.text = "Grenades: " + grenadeCount.ToString();
    }

}
