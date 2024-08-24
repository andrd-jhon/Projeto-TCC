using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeComponent : MonoBehaviour
{
    Image fadeImage;
    Color color;
    Color transparentColor;

    public float teste = 23f;

    [Range(0, 5)]
    [SerializeField] float outDuration = 1.5f;

    [Range(0, 5)]
    [SerializeField] float inDuration = 1.5f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        color = new Color(0, 0, 0, 1);
        transparentColor = new Color(0, 0, 0, 0);   
    }

    public IEnumerator Fade(Color a, Color b, float duration)
    {
        float timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            fadeImage.color = Color.Lerp(a, b, timeElapsed / duration);
            yield return null; 
        }
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Fade(color, transparentColor, outDuration));
        fadeImage.enabled = false;
        PlayerController.state = PLAYER.FREE;
    }

    public IEnumerator FadeIn()
    {
        fadeImage.enabled = true;
        PlayerController.state = PLAYER.INTERACT;
        yield return StartCoroutine(Fade(transparentColor, color, inDuration));
    }
}