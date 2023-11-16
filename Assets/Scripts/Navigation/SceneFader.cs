using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [Header("Stats")]
    public float fadeTimer = 1.5f;
    [Header("References")]
    public Image Fader;
    public AnimationCurve FadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = fadeTimer;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = FadeCurve.Evaluate(t);
            Fader.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0;

        while (t < fadeTimer)
        {
            t += Time.deltaTime;
            float a = FadeCurve.Evaluate(t);
            Fader.color = new Color(0, 0, 0, a);
            yield return null;
        }

        SceneManager.LoadScene(scene);
    }
}
