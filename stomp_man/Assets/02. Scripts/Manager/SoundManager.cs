using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance = null;
    public static SoundManager Instance => _instance;

    public enum Bgm
    {
        BGM_GAME,
    }

    public enum Sfx
    {
        STOMP,
        ENEMYEXPLOSION,
        ITEMPICKUP,
        GAMEOVER,
    }

    [SerializeField] private AudioClip[] bgms;
    [SerializeField] private AudioClip[] sfxs;

    [SerializeField] private AudioSource audioBgm;
    [SerializeField] private AudioSource audioSfx;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void PlayBGM(Bgm bgmIndex)
    {
        audioBgm.clip = bgms[(int)bgmIndex];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(Sfx sfxIndex)
    {
        audioSfx.PlayOneShot(sfxs[(int)sfxIndex]);
    }
}