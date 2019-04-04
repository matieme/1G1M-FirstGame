using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitterCollisionDetection : MonoBehaviour
{
    public LightEmitter lightEmitterParent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dark"))
        {
            lightEmitterParent.darkDisabledObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);           
        }
    }

}
