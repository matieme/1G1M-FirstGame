using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : SingletonObject<GameManager>, IObservable
    {
        private List<IObserver> _allObservers = new List<IObserver>();
        public Action<StoneTypes> pickUpStone;

        public Text fireCountDownText;

        float currCountdownValue;
        public float countDownValue;

        private void Awake()
        {
            pickUpStone += PickUpStone;
            Character.Instance.getFire += OnCharacterGetFire;
            Character.Instance.leftFire += OnCharacterLeftFire;
        }

        private void Update()
        {
            fireCountDownText.text = "0" + currCountdownValue.ToString();
        }

        private void OnCharacterGetFire()
        {
            StartCoroutine(StartCountdown(countDownValue));
        }

        private void OnCharacterLeftFire()
        {
            StopAllCoroutines();
            currCountdownValue = 0;
        }

        private void PickUpStone(StoneTypes stoneType)
        {
            Debug.LogError(stoneType);
        }

        public void Subscribe(IObserver observer)
        {
            if (!_allObservers.Contains(observer))
                _allObservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            if (_allObservers.Contains(observer))
                _allObservers.Remove(observer);
        }

        public IEnumerator StartCountdown(float countdownValue = 10)
        {
            currCountdownValue = countdownValue;
            while (currCountdownValue > 0)
            {
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }

            if (currCountdownValue == 0)
            {
                Debug.LogError("DEVOLVER EL FUEGO");
                if(Character.Instance.lastFirePosition!= null)
                {
                    Character.Instance.lastFirePosition.ReActivateLightEmitter();
                    Character.Instance.leftFire();
                }
            }
                
        }
    }
}

