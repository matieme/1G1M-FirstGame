using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelManager : MonoBehaviour
{
    public GameObject portal;
    public GameObject door;
    public AudioClip portalEffect;

    public void Activate()
    {
        portal.SetActive(true);
        door.GetComponent<Animator>().SetTrigger("Start");
        EZCameraShake.CameraShaker.Instance.ShakeOnce(0.5f, 9f, 0.2f, 3.6f);

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = portalEffect;
        audio.Play();
    }

}
