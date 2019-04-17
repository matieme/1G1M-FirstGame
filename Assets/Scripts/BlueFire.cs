using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : MonoBehaviour
{
    public string layer = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            Character.Instance.PlayerGetsFirstFire();
            gameObject.SetActive(false);
        }
    }
}
