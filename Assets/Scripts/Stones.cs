using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour
{
    public StoneTypes type;

    public string layer = "Player";

    public GameObject renderer;
    public GameObject effect;

    public AudioClip pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = pickUpEffect;
            audio.Play();

            renderer.SetActive(false);
            effect.SetActive(true);
            GameManager.Instance.pickUpStone(type);
            Destroy(gameObject,1);
        }
    }
}

public enum StoneTypes { Amatist, Ruby, Sapphire, Jade };