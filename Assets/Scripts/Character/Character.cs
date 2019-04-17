using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SingletonObject<Character>
{
    public GameObject fire;
    public Action getFire;
    public Action leftFire;

    public LightEmitter lastFirePosition;

    private bool _playerHasFire;
    public bool PlayerHasFire
    {
        get { return _playerHasFire; }

        set { _playerHasFire = value; }
    }

    public void PlayerGetsFire(LightEmitter lastFirePos)
    {
        lastFirePosition = lastFirePos;
        fire.SetActive(true);
        _playerHasFire = true;
        if (getFire != null)
            getFire();
    }

    public void PlayerGetsFirstFire()
    {
        fire.SetActive(true);
        _playerHasFire = true;
    }

    public void PlayerLeftFire()
    {
        fire.SetActive(false);
        _playerHasFire = false;
        if (leftFire != null)
            leftFire();
    }
}
