using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // this can also be used to spawn chests/jars that drop healing or other passive items
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnInterval; // seconds
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float speed = 1; // not scaled
    [SerializeField] float health = 50; // scaled
    [SerializeField] float damage = 5; // scaled
    //[SerializeField] int xp_amount = 10;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponentInParent<STGameManager>().getScaleWithWaveNumber())
        {
            int currentWave = transform.parent.GetComponentInParent<STGameManager>().getCurrentWave();
            float waveScale = transform.parent.GetComponentInParent<STGameManager>().getScaleDiff();
            float formula = (currentWave * waveScale);
            health = health + health * formula;
            damage = damage + damage * formula;
        }
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
        newEnemy.GetComponent<Enemy>().setSpeed(speed);
        newEnemy.GetComponent<Enemy>().setHealth(health);
        newEnemy.GetComponent<Enemy>().setDamage(damage);

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
