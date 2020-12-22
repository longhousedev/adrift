using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource music;
    private int canPlay;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        canPlay = PlayerPrefs.GetInt("Music");
        if (canPlay == 1) music.Play();
        AudioListener.volume = (PlayerPrefs.GetFloat("Volume") / 100);
    }

    // Update is called once per frame
    void Update()
    {
        canPlay = PlayerPrefs.GetInt("Music");
        if (canPlay == 1 && !music.isPlaying) music.Play();
        else if (canPlay == 0) music.Stop();
        AudioListener.volume = (PlayerPrefs.GetFloat("Volume") / 100);
    }
}
