using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Jing Liu
 *  Last Updated:   2022.07.20
 *  Version:        1.0
 */

public class STAudio: MonoBehaviour
{
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Shoot;
    [SerializeField] AudioSource GemPickup;
    [SerializeField] AudioSource Dying;
    [SerializeField] AudioSource Lose;
    [SerializeField] AudioSource Win;


    void Start() {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
    
    public void PlayShoot() {
        Shoot.Play();
    }

    public void PlayGemPickup() {
        GemPickup.Play();
    }

    public void PlayDying() {
        Dying.PlayOneShot(Dying.clip, 0.5f);
    }

    public void PlayLose() {
        Lose.Play();
    }

    public void PlayWin() {
        Win.Play();
    }
}
