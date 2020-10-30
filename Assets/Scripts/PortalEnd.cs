using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnd : MonoBehaviour
{
    public GameObject canvas;
    public GameObject endCanvas;

    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(false);
        endCanvas.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
