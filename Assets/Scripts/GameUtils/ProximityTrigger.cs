using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{

    public enum Layers { Dark, Player};
    public Layers layer;

    public Action<Collider> triggerEnter;
    public Action<Collider> triggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer.ToString()))
        {
            if (triggerEnter != null)
                triggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer.ToString()))
        {
            if (triggerExit != null)
                triggerExit(other);
        }
    }
}
