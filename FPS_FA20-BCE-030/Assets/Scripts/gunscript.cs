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
    public GameObject bullet;
    public AudioSource bulletaudio;

    private float maxHealth = 50;
    private float health;
    private int bulletCount = 20;

    public Image overlay;
    public float duration; //how long stays opaque
    public float fadeSpeed; //how fast fades
    private float durationTimer; //timer to check against duration

    private bool enemyAlive = true; // Flag to track if the enemy is alive
    private bool hasKey = false; // Flag to check if the key has been picked up

    void Awake()
    {
        health = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;

        UpdateBulletCountText();

        bulletaudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            ShootBullet();
        }
        if (overlay.color.a > 0) // Check if the alpha value is greater than 0
        {
            if (health < 10)
            {
                return;
            }
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    void ShootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "enemy")
            {
                //die animation
                enemyscript enemy = hit.transform.GetComponent<enemyscript>();
                enemy.die();
                enemyAlive = false; // Set the flag to false when the enemy dies
            }
        }
        bulletaudio.Play();
        bulletCount--;
        UpdateBulletCountText();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            print("Player hit");
            durationTimer = 0;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
            health -= 10;
            HealthBar.value = health;

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
                Debug.Log("Player died!");
            }
        }
        if (col.gameObject.CompareTag("boss"))
        {
            print("Player hit");
            durationTimer = 0;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
            health -= 10;
            HealthBar.value = health;

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
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

        if (col.gameObject.tag.StartsWith("win1"))
        {
            SceneManager.LoadScene("Level2");
        }

        if (col.gameObject.tag.StartsWith("key"))
        {
            hasKey = true; // Set the flag to true when the key is picked up
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag.StartsWith("win"))
        {
            if (hasKey)
            {
                SceneManager.LoadScene("menu");
            }
            else
            {
                Debug.Log("You need to pick up the key first!");
            }
        }
    }

    void UpdateBulletCountText()
    {
        bulletCountText.text = "Bullets: " + bulletCount.ToString();
    }
}
