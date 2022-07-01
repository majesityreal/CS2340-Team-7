using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio: MonoBehaviour
{
    public AudioSource Shuffle;
    public AudioSource DealCard;
    public AudioSource FlipCard;
    public AudioSource FlipCard2;
    public AudioSource PokerChip;
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
    public void PlayShuffle() {
        Shuffle.Play();
    }

    public void PlayDealCard() {
        DealCard.Play();
    }

    public void PlayFlipCard() {
        FlipCard.Play();
    }

    public void PlayFlipCard2() {
        FlipCard2.Play();
    }
    public void PlayPokerChip() {
        PokerChip.Play();
    }
}
