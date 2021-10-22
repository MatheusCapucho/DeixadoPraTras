using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cam;

    private float levelOffset = 15f;

    [SerializeField] public Collider2D[] colSize;

    public float[] xBounds;

    private GameObject player;

    [SerializeField] private Renderer playerRenderer;

    private bool wait = false;

    int aux = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        cam = Camera.main;
        xBounds = new float[colSize.Length];
        int i = -1;
        foreach (Collider2D col in colSize)
        {
            i++;
            xBounds[i] = col.bounds.center.x;
        }

        StartCoroutine(Conf());

    }

    IEnumerator Conf()
    {
        wait = false;
        yield return new WaitForSeconds(1f);
        wait = true;
    }

    private void FixedUpdate()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(player.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (!onScreen && wait)
        {
            MoveCamera(aux);
            aux++;
        } 
        else
        {
            aux = 0;
        }

    }

    public void MoveCamera(int level)
    {
        cam.transform.position = new Vector3(xBounds[level], 0, -10);
        EndLevel.levelCount = level;
    }

    public void MovePortals(int level)
    {
        GameObject portal1 = GameObject.Find("Portal0");
        GameObject portal2 = GameObject.Find("Portal1");

        if (portal1 != null)
            portal1.transform.position += cam.transform.position;
        if (portal2 != null)
            portal2.transform.position += cam.transform.position;
    }

}
