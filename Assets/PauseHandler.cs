using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    private fade f;
    // Start is called before the first frame update
    void Start()
    {
        f = gameObject.GetComponent<fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            //f.PauseFadeIn(0.5f);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        //f.PauseFadeOut(0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Save()
    {
        
    }

    public void Quit()
    {
        
    }
}
