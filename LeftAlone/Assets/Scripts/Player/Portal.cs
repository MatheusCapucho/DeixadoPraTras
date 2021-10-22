using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject otherPortal;
    private GameObject player;
    private static int maxPortals = 0;

    private float nextPortalTrigger = 0f;
    [SerializeField] private float cooldown = 1f;

    private CameraControl cameraControl;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraControl = Camera.main.GetComponent<CameraControl>();

        if (maxPortals % 2 == 1)
            otherPortal = GameObject.Find("Portal0");
        else
            otherPortal = GameObject.Find("Portal1");

        StartCoroutine(Config());

        maxPortals++;
    }

    public void MovePortal(bool toRight)
    {
        if (toRight)
        {
            float distance = cameraControl.xBounds[EndLevel.levelCount] - cameraControl.xBounds[EndLevel.levelCount - 1];
            this.gameObject.transform.position += new Vector3(distance, 0, 0);
        }
        else
        {
            float distance = cameraControl.xBounds[EndLevel.levelCount + 1] - cameraControl.xBounds[EndLevel.levelCount];
            this.gameObject.transform.position -= new Vector3(distance, 0, 0);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPortal == null)
            return;

        if (other.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            return;

        var col = otherPortal.GetComponent<Collider2D>();
        col.enabled = false;
        if (Time.timeSinceLevelLoad > nextPortalTrigger && maxPortals > 1) {
            nextPortalTrigger = cooldown + Time.timeSinceLevelLoad;
            StartCoroutine(Teleport());
        }
    }

    public void StartCr()
    {
        StartCoroutine(Config());
    }
    IEnumerator Config()
    {     
        Collider2D col = GetComponent<Collider2D>();

        yield return new WaitForSeconds(2f);

        col.enabled = true;  
    }

    IEnumerator Teleport()
    {

        otherPortal.GetComponent<Collider2D>().enabled = false;

        player.transform.position = otherPortal.transform.position;

        yield return new WaitForSeconds(.5f);

        otherPortal.GetComponent<Collider2D>().enabled = true;
    }

    public void ChangePortalReference(GameObject newPortal)
    {
        otherPortal = newPortal;
    }

}
