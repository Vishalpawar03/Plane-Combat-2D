using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    public PlayerHealthBar healthBar;
    public float padding = 0.8f;
    float minX, maxX, minY, maxY;
    public GameObject playerExplosionPrefab;
    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;
    public GameObject playerDamageEffect;
    public GameController gameController;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        FindBoundries();
        damage = barFillAmount / health;    
    }

    void FindBoundries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        // float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        // float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        // float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        // float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        // transform.position = new Vector2(newXpos, newYpos);

        if(Input.GetMouseButton(0)) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = Vector2.Lerp(transform.position, newPos, 10*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet"){
            audioSource.PlayOneShot(damageSound, 0.5f);
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVFX = Instantiate(playerDamageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVFX, 0.05f);
            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
                gameController.GameOver();
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject playerExplosion = Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(playerExplosion, 2f);
            }
        }
    }
    void DamagePlayerHealthbar()
    {
        if(health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            healthBar.SetAmount(barFillAmount);
        }
    }
}
