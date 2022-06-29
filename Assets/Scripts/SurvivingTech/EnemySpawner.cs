using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnInterval; // seconds
    [SerializeField] GameObject targetPlayer;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }
    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0f);
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPosition;
        newEnemy.GetComponent<Enemy>().setTarget(targetPlayer);
    }
}
