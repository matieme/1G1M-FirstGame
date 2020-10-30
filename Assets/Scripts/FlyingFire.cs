using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFire : MonoBehaviour
{
    public float speed = 4;
    private Transform target;
    private bool isInitialized;
    private float distToTarget;
    public Action OnFlyingFireArrive;

    public void Setup(Transform targetPos)
    {
        target = targetPos;
        isInitialized = true;
        transform.LookAt(target);
    }
    
    void Update()
    {
        distToTarget = Vector3.Distance(target.position, transform.position);

        if (isInitialized)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if(distToTarget < 0.3f)
        {
            if (OnFlyingFireArrive != null)
                OnFlyingFireArrive();

            Destroy(this.gameObject);
        }
    }
}
