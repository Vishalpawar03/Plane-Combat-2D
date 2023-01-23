using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform []gunPoint;
    public float bulletSpawnTime = 0.5f;
    public GameObject explosionPrefab;
    public GameObject damageEffect;
    public float health = 5f;
    float barSize = 1f;
    float damage = 0;
    public EnemyHealthbar healthbar;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip shootingSound;
    public AudioClip damageSound;
    //public Score scoreScript;
    // public TMP_Text scoreText;
    // public TMP_Text finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            audioSource.PlayOneShot(damageSound, 0.5f);
            DamageHealthBar();
            Destroy(collision.gameObject);

            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
                GameObject damageVFX = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
                Destroy(damageVFX, 0.1f);
                Destroy(gameObject);
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
                //scoreScript.AddCount();
            }
        }
    }

    void Fire()
    {
        for(int i = 0; i < gunPoint.Length; i++)
        {
           Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity); 
        }
        // Instantiate(enemyBullet, spawnPoint1.position, Quaternion.identity);
        // Instantiate(enemyBullet, spawnPoint2.position, Quaternion.identity);
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            audioSource.PlayOneShot(shootingSound, 0.5f);
            yield return new WaitForSeconds(bulletSpawnTime);
            Fire();
        }
    }
    void DamageHealthBar()
    {
        if(health > 0)
        {
            health -= 1;
            barSize -= damage;
            healthbar.SetSize(barSize);
        }
    }
}
