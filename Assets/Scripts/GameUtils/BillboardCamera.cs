using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCamera : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject graphics;
    public ProximityTrigger myTrigger;

    private void Start()
    {
        myTrigger.triggerEnter += OnPlayerEnter;
        myTrigger.triggerExit += OnPlayerExit;
    }

    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    private void OnPlayerEnter(Collider other)
    {
        graphics.SetActive(true);
    }

    private void OnPlayerExit(Collider other)
    {
        graphics.SetActive(false);
    }

    private void OnDestroy()
    {
        myTrigger.triggerEnter -= OnPlayerEnter;
        myTrigger.triggerExit -= OnPlayerExit;
    }
}
