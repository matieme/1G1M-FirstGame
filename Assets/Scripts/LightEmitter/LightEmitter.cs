using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : MonoBehaviour
{ 
    public GameObject collider;
    private Action turnEnded;
    public GameObject turnGraphics;
    public GameObject activateGraphics;

    public List<GameObject> darkDisabledObjects;

    public KeyCode rightKey;
    public KeyCode LeftKey;
    public KeyCode fireKey;

    public ProximityTrigger proximityChecker;

    public Animator activeFire;

    public AudioClip fireEffect;
    private AudioSource audioEmitter;

    private bool isPlayerInRange;
    private bool isRotating;
    private bool imActive;
    private bool canActivate;

    private void Start()
    {
        turnEnded += ActivateCollider;
        proximityChecker.triggerEnter += PlayerInRange;
        proximityChecker.triggerExit += PlayerOutOfRange;

        audioEmitter = GetComponent<AudioSource>();
        audioEmitter.clip = fireEffect;
    }

    public void Update()
    {
        if(!isRotating && isPlayerInRange)
        {
            if (imActive)
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

            if (!canActivate && imActive && !Character.Instance.PlayerHasFire)
            {
                if (Input.GetKeyDown(fireKey))
                {
                    DeActivateLightEmitter();
                }
            }else if (canActivate && Character.Instance.PlayerHasFire)
            {
                if (Input.GetKeyDown(fireKey))
                {
                    ActivateLightEmitter();
                }
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

        if(imActive){
            turnGraphics.SetActive(true);
        }
        else{
            if(Character.Instance.PlayerHasFire)
            {
                activateGraphics.SetActive(true);
                canActivate = true;
            }    
        }
    }

    private void PlayerOutOfRange(Collider obj)
    {
        isPlayerInRange = false;

        if (imActive){
            turnGraphics.SetActive(false);
        }
        else{
            activateGraphics.SetActive(false);
        }
    }

    public void ReActivateLightEmitter()
    {
        ActivateLightEmitter();
    }

    private void ActivateLightEmitter()
    {
        imActive = true;
        canActivate = false;
        activateGraphics.SetActive(false);
        turnGraphics.SetActive(true);
        activeFire.SetTrigger("Play");
        Character.Instance.PlayerLeftFire();
        audioEmitter.Play();
    }

    private void DeActivateLightEmitter()
    {
        imActive = false;
        canActivate = true;
        activateGraphics.SetActive(true);
        turnGraphics.SetActive(false);
        activeFire.SetTrigger("Stop");
        Character.Instance.PlayerGetsFire(this);
    }

    IEnumerator RotateAround(Vector3 axis, float angle, float duration)
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(0.3f, 8f, 0.1f, 2.6f);
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
