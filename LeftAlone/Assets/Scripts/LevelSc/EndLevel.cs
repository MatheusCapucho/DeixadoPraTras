using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private static int currentScene;
    [SerializeField] private bool isEndLevel;
    [SerializeField] private bool isEndScene;
    [SerializeField] private CameraControl cameraControl;
    public static int levelCount = 0;

    private void Awake()
    {
        if(isEndLevel)
            StartCoroutine(Config(1));
        else
            StartCoroutine(Config(3));
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
    public void StartCr()
    {
        StartCoroutine(Config(3));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

            if (!isEndScene)
            {
                if (isEndLevel)
                {
                    cameraControl.MoveCamera(++levelCount);
                    other.GetComponent<PlayerMovement>().startPos[levelCount].GetComponent<Collider2D>().enabled = false;
                    other.gameObject.transform.position = other.GetComponent<PlayerMovement>().startPos[levelCount].transform.position;
                    other.GetComponent<PlayerMovement>().startPos[levelCount].GetComponent<EndLevel>().StartCr();
                    foreach (var portal in portals)
                    {
                        portal.GetComponent<Portal>().MovePortal(true);
                    }
                    

                }
                else if (levelCount != 0)
                {
                    cameraControl.MoveCamera(--levelCount);
                    other.GetComponent<PlayerMovement>().startPos[levelCount].GetComponent<Collider2D>().enabled = false;
                    other.gameObject.transform.position = other.GetComponent<PlayerMovement>().startPos[levelCount].transform.position;
                    other.GetComponent<PlayerMovement>().startPos[levelCount].GetComponent<EndLevel>().StartCr();
                    other.gameObject.GetComponent<PlayerMovement>()._isMoving = false;
                    foreach (var portal in portals)
                    {
                        portal.GetComponent<Portal>().MovePortal(false);
                    }
                }
            } else
            {
                SceneManager.LoadScene("TheEnd");
            }
        }        

    }
}
