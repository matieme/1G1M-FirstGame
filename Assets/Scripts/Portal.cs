using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int newScene; 

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.OnChangeScene(newScene);
    }
}