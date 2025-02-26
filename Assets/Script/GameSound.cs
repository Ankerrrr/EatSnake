using UnityEngine;

public class GameSound : MonoBehaviour
{
    public AudioSource bgXiaoBa;
    public AudioSource eatSound;
    public AudioSource touchTailSound;
    public AudioSource loose;

    public GameUI gameui;

    bool playingBg = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayBG()
    {
        if (!playingBg)
        {
            Debug.Log("Playing");
            bgXiaoBa.time = 0;
            playingBg = true;
            bgXiaoBa.Play();
        }
    }

    public void BGMStop()
    {
        bgXiaoBa.Stop();
        Debug.Log("StopMusic");
        playingBg = false;
        bgXiaoBa.pitch = 0;
    }

    public void BGraisePitch()
    {
        bgXiaoBa.pitch += 0.05f;
    }

    public void PlayEatSound()
    {
        eatSound.Play();
    }

    public void PlaytouchTailSound()
    {
        touchTailSound.Play();
    }

    public void PlayLooseMusic()
    {
        loose.Play();
    }
}
