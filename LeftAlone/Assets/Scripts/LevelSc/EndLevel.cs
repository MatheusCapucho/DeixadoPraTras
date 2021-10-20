using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private static int currentScene;
    [SerializeField] private bool isEndLevel;

    private void Awake()
    {
        if(isEndLevel)
            StartCoroutine(Config(3));
        else
            StartCoroutine(Config(5));
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator Config(float x)
    {
        yield return new WaitForSeconds(x);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        
        yield return null;
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isEndLevel)
                SceneManager.LoadScene(++currentScene);
            else if (SceneManager.GetActiveScene().name != "Level0")
                SceneManager.LoadScene(--currentScene);
        }        

    }
}
