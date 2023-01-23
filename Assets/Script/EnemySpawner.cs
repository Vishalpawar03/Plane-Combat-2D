using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject []enemy;
    public float respawnTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length);
        int randomXpos = Random.Range(-2, 2);
        Instantiate(enemy[randomValue], new Vector2(randomXpos, 6), Quaternion.identity);
    }
}
