using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletSpawnTime = 0.5f;
    public Score scoreScript;
    public GameObject playerBullet;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire() {
        Instantiate(playerBullet, spawnPoint1.position, Quaternion.identity);
        Instantiate(playerBullet, spawnPoint2.position, Quaternion.identity);
        audioSource.Play();
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(bulletSpawnTime);
        Fire();
        StartCoroutine(Shoot());
        scoreScript.AddCount();
    }
}
