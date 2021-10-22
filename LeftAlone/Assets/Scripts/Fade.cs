using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Image spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<Image>();
        if (SceneManager.GetActiveScene().name == "TheEnd")
            StartCoroutine(End());
        else
            StartCoroutine(FadeIm());
    }
    IEnumerator End()
    {
        while (spriteRenderer.color.a > 0)
        {
            yield return new WaitForSeconds(.1f);
            spriteRenderer.color -= new Color(0, 0, 0, .1f);
        }

        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("Menu");

        yield return null;
    }

    IEnumerator FadeIm()
    {
        while (spriteRenderer.color.a > 0)
        {
            yield return new WaitForSeconds(.1f);
            spriteRenderer.color -= new Color(0, 0, 0, .1f);
        }

        yield return null;
    }

}
