using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class STGameManager : MonoBehaviour
{
    [SerializeField] GameObject[] waves;
    private int currentWave = 0;
    [SerializeField] float timeBetweenWaves = 60f;
    [SerializeField] float totalTimeBattle = 600f; // in seconds, duration of the game
    float timer;
    float totalSeconds;
    TimeSpan time;
    [SerializeField] bool isGameStarted = false;
    [SerializeField] bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isGameStarted) {
        StartWave(waves[currentWave]);
        timer = timeBetweenWaves;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted && !isGameOver) {
            timer -= Time.deltaTime;
            if (timer <= 0 && currentWave < waves.Length - 1) // dont stop spawning last wave
            {
                
                StopWave(waves[currentWave]);
                currentWave++;
                if (currentWave < waves.Length){
                StartWave(waves[currentWave]);
                timer = timeBetweenWaves;}
            }
            totalSeconds += Time.deltaTime;
            time = TimeSpan.FromSeconds(totalSeconds);
            Debug.Log(time.ToString("hh':'mm':'ss"));
            // final wave will keep spawning until 10 minutes have passed
            if (currentWave == waves.Length - 1 && totalSeconds > totalTimeBattle)
            {
                StopWave(waves[currentWave]);
                GameOver();
            }
        }
    }
    void StartWave(GameObject wave)
    {
        Debug.Log("Spawning wave " + currentWave);
        wave.SetActive(true);
    }
    void StopWave(GameObject wave)
    {
        wave.SetActive(false);
    }
    public void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
    }
}
