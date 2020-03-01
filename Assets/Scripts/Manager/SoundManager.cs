/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    [SerializeField]
    public AudioClip shoot_1;
    [SerializeField]
    public AudioClip shoot_2;
    [SerializeField]
    public AudioClip shoot_3;
    [SerializeField]
    public AudioClip shoot_4;
    [SerializeField]
    public AudioClip shoot_5;
    [SerializeField]
    public AudioClip shoot_6;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioSource backGroundSound;
    [SerializeField]
    public AudioClip endGame;
    [SerializeField]
    public AudioClip Explode_1;
    [SerializeField]
    public AudioClip Explode_2;

    public float volSound;
    public float volSFX;

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

	}

    void Start()
    {
        updateVol();
    }

    public void updateVol()
    {
        volSound = PlayerPrefs.GetFloat(MenuScript.SOUND_KEY);
        volSFX = PlayerPrefs.GetFloat(MenuScript.SFX_KEY);
        soundBackground();
    }

    public void playSoundShoot_1()
    {
        playAudioClip(shoot_1,volSFX);
    }

    public void playSoundShoot_2()
    {
        playAudioClip(shoot_2,volSFX);
    }

    public void playSoundShoot_3()
    {
        playAudioClip(shoot_3, volSFX);
    }

    public void playSoundShoot_4()
    {
        playAudioClip(shoot_4, volSFX);
    }

    public void playSoundShoot_5()
    {
        playAudioClip(shoot_5, volSFX);
    }

    public void playSoundShoot_6()
    {
        playAudioClip(shoot_6, volSFX);
    }

    public void playSoundEndGame()
    {
        playAudioClip(endGame, volSFX);
    }

    public void soundBackground()
    {
        backGroundSound.volume = volSound;
    }

    public void playSoundExplode_1()
    {
        playAudioClip(Explode_1, volSFX);
    }

    public void playSoundExplode_2()
    {
        playAudioClip(Explode_2, volSFX);
    }

    void playAudioClip(AudioClip audio,float vol)
    {
        audioSource.PlayOneShot(audio, vol);
    }

    // Update is called once per frame
    public void updateSound () {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioSource[] audio = FindObjectsOfType<AudioSource>();
            foreach(AudioSource ad in audio)
            {
                ad.mute = false;
            }
        }
        else
        {
            AudioSource[] audio = FindObjectsOfType<AudioSource>();
            foreach (AudioSource ad in audio)
            {
                ad.mute = true;
            }
        }
    }
}
