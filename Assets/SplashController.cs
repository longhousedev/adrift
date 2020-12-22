using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    private fade f;
    // Start is called before the first frame update
    void Start()
    {
        f = GameObject.FindGameObjectWithTag("Fader").GetComponent<fade>();
        StartCoroutine(nameof(Splash));
    }

    // Update is called once per frame

    IEnumerator Splash()
    {
        float elapsed = 0;
        float time = 1;
        gameObject.GetComponent<AudioSource>().Play();
        while (elapsed < time)
        {
            f.FadeOut(1, 1);
            yield return new WaitForSeconds(1);
            elapsed++;
        }

        SceneManager.LoadScene(1);
        
    }
}
