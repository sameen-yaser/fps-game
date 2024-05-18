using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class maincScript : MonoBehaviour
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
    private Vector3 respawnPosition = new Vector3(245.5f, 15.8f, 827.6f); // Respawn position
    private bool checkpointReached = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cp"))
        {
            Debug.Log("CPHIT");
            checkpointReached = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("ebullet"))
        {
            print("Player hit");
            health -= 5;
            HealthBar.value = health;

            if (health <= 0)
            {
                // Player is dead, respawn
                Respawn();
                Debug.Log("Player died and respawned!");
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

    void Respawn()
    {
        if (checkpointReached)
        {
            SceneManager.LoadScene("checkpoint");
            //load checkpoint scene where player starts from checkpoint
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //if player dies without reaching checkpoint, reload game
        }
    }
}
