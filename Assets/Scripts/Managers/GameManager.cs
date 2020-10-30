using GameUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public ProximityTrigger deathZone;
        public EndLevelManager endLevelManager;

        public FlyingFire flyingFirePrefab;
        private FlyingFire flyingFire;

        private List<StoneTypes> stonesBag;

        public GameObject canvas;
        public GameObject loadingCanvas;

        private void Awake()
        {
            pickUpStone += PickUpStone;
            Character.Instance.getFire += OnCharacterGetFire;
            Character.Instance.leftFire += OnCharacterLeftFire;
            deathZone.triggerEnter += OnDeathZone;
            stonesBag = new List<StoneTypes>();
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

        private void OnDeathZone(Collider obj)
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.buildIndex);
        }

        public void OnChangeScene(int scene)
        {
            canvas.SetActive(false);
            loadingCanvas.SetActive(true);
            StartCoroutine(ChangeSceneNow(scene, 1.5f));            
        }

        IEnumerator ChangeSceneNow(int scene, float timer)
        {
            yield return new WaitForSeconds(timer);
            SceneManager.LoadScene(scene);
        }

        private void PickUpStone(StoneTypes stoneType)
        {
            Debug.LogError(stoneType);
            if(!stonesBag.Contains(stoneType))
            {
                stonesBag.Add(stoneType);
            }

            if (stonesBag.Count == 4){
                stonesBag.Clear();
                ActivatePortal();
            }
        }

        private void ActivatePortal()
        {
            endLevelManager.Activate();
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
                    flyingFire = Instantiate(flyingFirePrefab);
                    flyingFire.transform.position = Character.Instance.transform.position;
                    flyingFire.Setup(Character.Instance.lastFirePosition.transform);
                    flyingFire.OnFlyingFireArrive += ReActivateLightEmitter;
                    Character.Instance.PlayerLeftFire();

                }
            }
                
        }

        private void ReActivateLightEmitter()
        {
            Character.Instance.leftFire();
            Character.Instance.lastFirePosition.ReActivateLightEmitter();
            flyingFire.OnFlyingFireArrive -= ReActivateLightEmitter;
        }
    }
}

