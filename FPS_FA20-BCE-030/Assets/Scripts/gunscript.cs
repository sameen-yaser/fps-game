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

    void Awake()
    {
        health = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;

        UpdateBulletCountText();

        bulletaudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            ShootBullet();
        }

    }

    void ShootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))

            {
                Debug.Log("Hit: " + hit.collider.name);
            if (hit.transform.tag == "enemy")
            {
                //die animation
                enemyscript enemy = hit.transform.GetComponent<enemyscript>();
                enemy.die();
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
            health -= 10;
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

        if (col.gameObject.tag.StartsWith("win"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateBulletCountText()
    {
        bulletCountText.text = "Bullets: " + bulletCount.ToString();
    }


}
