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
        Vector3 spawnPosition = GenerateRandomPosition();
        spawnPosition += targetPlayer.transform.position; // so enemy will spawn relative to player position
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPosition;
        newEnemy.GetComponent<Enemy>().setTarget(targetPlayer);
        // for cleaning up, but leave this commented because i have an idea for handling waves
        // newEnemy.transform.parent = transform;
    }

    Vector3 GenerateRandomPosition() // this is to generate enemy off screen
    {
        Vector3 spawnPosition = new Vector3();
        float maxValue = Random.value > 0.5f ? -1f : 1f;
        if (Random.value > 0.5f) {
            spawnPosition.x = Random.Range(-spawnArea.x, spawnArea.x);
            spawnPosition.y = spawnArea.y * maxValue;
        } else {
            spawnPosition.y = Random.Range(-spawnArea.y, spawnArea.y);
            spawnPosition.x = spawnArea.x * maxValue;
        }
        spawnPosition.z = 0;
        return spawnPosition;
    }
}
