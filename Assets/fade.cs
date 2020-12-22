using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fade : MonoBehaviour
{
    public Image image;
    private float  _alpha;

    // Start is called before the first frame update
    public void FadeOut(float duration, int sceneNo)
    {
        StartCoroutine(FadeOutCo(duration, sceneNo));
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(FadeInCo(duration));
    }

    public void PauseFadeIn(float duration)
    {
        StartCoroutine(PauseFadeInCo(duration));
    }

    public void PauseFadeOut(float duration)
    {
        StartCoroutine(PauseFadeOutCo(duration));
    }
    
    IEnumerator FadeOutCo(float duration, int sceneNo)
    {
        image.enabled = true;
        float elapsed = 0;
        while (elapsed < duration)
        {
            _alpha = Mathf.Lerp(0, 1, elapsed / duration);
            image.color = new Color(0, 0, 0, _alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene(sceneNo);
    }

    IEnumerator FadeInCo(float duration)
    {
        image.enabled = true;
        float elapsed = 0;
        while (elapsed < duration)
        {
            _alpha = Mathf.Lerp(1, 0, elapsed / duration);
            image.color = new Color(0, 0, 0, _alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 0);
        image.enabled = false;
    }

    IEnumerator PauseFadeInCo(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            _alpha = Mathf.Lerp(0.75f, 0, elapsed / duration);
            image.color = new Color(0, 0, 0, _alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 0);
    }
    
    IEnumerator PauseFadeOutCo(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            _alpha = Mathf.Lerp(0, 0.75f, elapsed / duration);
            image.color = new Color(0, 0, 0, _alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 0.75f);
    }
}
