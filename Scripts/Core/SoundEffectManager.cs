using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] notes;
    public bool bMuteNote;

    public AudioClip[] combines;
    public bool bMuteCombine;

    public AudioClip[] replace;
    public bool bMuteReplace;

    public AudioClip alert;
    public bool bMuteAlert;


    public AudioClip fullSleep;
    public AudioClip[] bgmList;
    public bool bMuteBGM;

    public int bgmIndex = 0;

    public Button btnMuteBGM;

    AudioSource aus;

    AudioSource bgmAus;

    private void Start()
    {
        aus = GetComponent<AudioSource>();

        bMuteBGM = false;
        bgmAus = gameObject.AddComponent<AudioSource>();
        bgmAus.loop = true;
        bgmAus.volume = 0.3f;
        bgmAus.clip = bgmList[bgmIndex];
        bgmAus.Play();

        btnMuteBGM.onClick.AddListener(() => BGMVolume());

    }

    public void BGMVolume()
    {
        bMuteBGM = !bMuteBGM;

        if (bMuteBGM)
            bgmAus.volume = 0;
        else
            bgmAus.volume = 0.3f;
    }

    public void BGMVolume(float value)
    {
        bgmAus.volume = value;
    }


    public void PlayNote()
    {
        if (bMuteNote)
            return;

        aus.PlayOneShot(notes[Random.Range(0, notes.Length)]);
    }


    public void PlayCombine()
    {
        if (bMuteCombine)
            return;

        aus.PlayOneShot(combines[Random.Range(0, combines.Length)]);
    }

    public void PlayWhenFullSleep()
    {

        aus.PlayOneShot(fullSleep);
    }

    public void PlayeReplace()
    {

        if (bMuteReplace)
            return;

        aus.PlayOneShot(replace[Random.Range(0, replace.Length)]);


    }

    public void PlayAlert()
    {
        if (bMuteAlert)
            return;

        aus.PlayOneShot(alert);
    }

}
