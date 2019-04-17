using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour
{
    public StoneTypes type;

    public string layer = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            GameManager.Instance.pickUpStone(type);
            Destroy(gameObject);
        }
    }
}

public enum StoneTypes { Amatist, Ruby, Sapphire, Jade };