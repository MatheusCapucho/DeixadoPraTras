using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenScenes = 5f;
    [SerializeField]
    private Sprite[] sprite;
    private Image image;

    void Start()
    {
        image = GetComponentInChildren<Image>();
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        for (int i = 0; i < sprite.Length; i++)
        { 
            image.sprite = sprite[i];
            yield return new WaitForSeconds(timeBetweenScenes);
        }

        SceneManager.LoadScene("Level0");

        yield return null;
    }

}
