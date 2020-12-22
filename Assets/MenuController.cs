using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject options;
    private GameObject fader;
    private fade f;

    private void Start()
    {
        f = GameObject.FindWithTag("Fader").GetComponent<fade>();
        fader = GameObject.FindWithTag("Fader");
        f.FadeIn(0.5f);
    }

    public void NewGame()
    {
        f.FadeOut(0.5f, 2);
    }

    public void LoadGame()
    {
        
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MakeSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
