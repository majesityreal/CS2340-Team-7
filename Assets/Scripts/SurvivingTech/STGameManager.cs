using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.12
 *  Version:        1.4
 */

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
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] bool scaleWithWaveNumber = true; // scale all enemies' health and damage with wave number and multiplier
    [SerializeField] float scalingDifficultyMultiplierPerWave = 0.5f; // 1f = no scaling, 2f = twice as hard, 0.5f = half as hard... linear
    // formula to calculate difficulty: health = health * scalingDifficultyMultiplierPerWave * waveNumber
    // Start is called before the first frame update
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject Player;


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
            //Debug.Log(time.ToString("hh':'mm':'ss"));
            timerText.text = time.ToString("mm':'ss");
            // final wave will keep spawning until 10 minutes have passed
            if (totalSeconds > totalTimeBattle)
            {
                StopWave(waves[currentWave]);
                WinScreen.SetActive(true);
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
        // note that stopping a wave doesn't mean that the wave is over, it just means that it is not spawning anymore
        // so enemies that are spawned from the wave that aren't killed yet will remain in play
    }
    public void StartGame()
    {
        Player.SetActive(true);
        isGameStarted = true;
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
    }
    public String getTime()
    {
        return time.ToString("hh':'mm':'ss");
    }

    public int getCurrentWave()
    {
        return currentWave;
    }
    public int getTotalWaves()
    {
        return waves.Length;
    }
    public float getTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }
    public float getScaleDiff()
    {
        return scalingDifficultyMultiplierPerWave;
    }
    public bool getScaleWithWaveNumber()
    {
        return scaleWithWaveNumber;
    }
}
