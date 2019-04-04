using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : MonoBehaviour
{ 
    public GameObject collider;
    private Action turnEnded;

    private bool isRotating;

    public List<GameObject> darkDisabledObjects;

    public KeyCode rightKey;
    public KeyCode LeftKey;

    public ProximityTrigger proximityChecker;

    private bool isPlayerInRange;

    private void Start()
    {
        turnEnded += ActivateCollider;
        proximityChecker.triggerEnter += PlayerInRange;
        proximityChecker.triggerExit += PlayerOutOfRange;
    }

    public void Update()
    {
        if(!isRotating && isPlayerInRange)
        {
            if (Input.GetKeyDown(rightKey))
            {
                StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
            }
            if (Input.GetKeyDown(LeftKey))
            {
                StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));
            }
        }
    }

    private void ActivateCollider()
    {
        collider.SetActive(true);
        isRotating = false;
    }    

    private void DeActivateCollider()
    {
        collider.SetActive(false);
        isRotating = true;
    }

    private void PlayerInRange(Collider obj)
    {
        isPlayerInRange = true;
    }

    private void PlayerOutOfRange(Collider obj)
    {
        isPlayerInRange = false;
    }

    IEnumerator RotateAround(Vector3 axis, float angle, float duration)
    {
        DeActivateCollider();
        ReactivatePreviousDarkZone();

        float elapsed = 0.0f;
        float rotated = 0.0f;
        while (elapsed < duration)
        {
            float step = angle / duration * Time.deltaTime;
            transform.RotateAround(transform.position, axis, step);
            elapsed += Time.deltaTime;
            rotated += step;
            yield return null;
        }
        transform.RotateAround(transform.position, axis, angle - rotated);
        if (turnEnded != null)
            turnEnded();
    }

    private void ReactivatePreviousDarkZone()
    {
        List<GameObject> tempList = darkDisabledObjects;
        foreach (var darkObject in tempList.ToArray())
        {
            darkObject.SetActive(true);
            tempList.Remove(darkObject);
        }
    }
}
