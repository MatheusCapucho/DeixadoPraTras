using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{

    [SerializeField] private GameObject _portalPrefab;

    private int _portalCount = 0;
    private int _portalIndex = 1;
    private Vector3[] _portalPosition = new Vector3[2];
    private GameObject _player;
    private GameObject[] _portal = new GameObject[2];

    private float cooldown = 1.4f;
    private float nextTime = 0f;

    [SerializeField] private CameraControl cameraControl;

    void Start()
    {
       _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.timeSinceLevelLoad > nextTime)
        {
 
            nextTime = Time.timeSinceLevelLoad + cooldown;

            _portalIndex = (_portalIndex == 1) ?  0 :  1;
            InstantiatePortal();

        }
    }
    private void InstantiatePortal()
    {
        if (_portalCount < 2)
            _portalCount++;
        _portalPosition[_portalIndex] = _player.transform.position;
        DestroyOlderPortal();
        _portal[_portalIndex] = Instantiate(_portalPrefab, _portalPosition[_portalIndex], Quaternion.identity);
        _portal[_portalIndex].name = "Portal" + _portalIndex;

        ChangeOlderPortalReference();
    }

    private void DestroyOlderPortal()
    {
        if (_portalCount < 2)      
            return;      

        Destroy(_portal[_portalIndex]);
    }
    private void ChangeOlderPortalReference()
    {
        if (_portalCount < 2)
            return;
        if (_portalIndex == 1)      
            _portal[0].GetComponent<Portal>().ChangePortalReference(_portal[1]);
        else
            _portal[1].GetComponent<Portal>().ChangePortalReference(_portal[0]);

    }

}
