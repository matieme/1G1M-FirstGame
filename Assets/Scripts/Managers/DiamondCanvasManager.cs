using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCanvasManager : MonoBehaviour
{

    public GameObject jadeOn;
    public GameObject amatistaOn;
    public GameObject rubyOn;
    public GameObject sapphireOn;

    public void Awake()
    {
        GameManager.Instance.pickUpStone += OnPickUpStone;
    }

    private void OnPickUpStone(StoneTypes stone)
    {
        switch (stone)
        {
            case StoneTypes.Amatist:
                amatistaOn.SetActive(true);
                break;
            case StoneTypes.Ruby:
                rubyOn.SetActive(true);
                break;
            case StoneTypes.Sapphire:
                sapphireOn.SetActive(true);
                break;
            case StoneTypes.Jade:
                jadeOn.SetActive(true);
                break;
            default:
                break;
        }
    }
}
