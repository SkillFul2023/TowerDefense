using System.Collections;
using UnityEngine;

namespace TowerDefense
{
    public class RespaunScripts : MonoBehaviour
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private GameObject _mob;
        [SerializeField] private GameObject _respaun;
        [SerializeField] private int _currentWave;
        [SerializeField] private bool _respaunOn = false;
        [SerializeField] private bool _waveOn = false;
        [SerializeField] private int _countMobInWave;
        [SerializeField] private int _countRespMob;
        [SerializeField] private float _countRespaunMobInSec;
        [SerializeField] private float _timer;
        [SerializeField] private MobManager[] _arrayMob;

        private void Awake()
        {
            _waveManager = FindObjectOfType<WaveManager>();
            _currentWave = _waveManager.CurrentWave;
            UpdateWaveConfiguration(_currentWave);
        }

        private IEnumerator CreaateMob(GameObject gameObject)
        {
            Instantiate(gameObject, _respaun.transform);
            yield return null;
        }
        private void Update()
        {
            _mob = _waveManager.GetMobModel;
            _waveOn = _waveManager.WaveOn;
            _respaunOn = _waveOn;
            _currentWave = _waveManager.CurrentWave;
            if (_waveOn == true && _respaunOn == true)
            {
                _timer = _timer + Time.deltaTime;

                if (_timer >= 1 / _countRespaunMobInSec & _countRespMob < _countMobInWave)
                {
                    StartCoroutine(CreaateMob(_mob));
                    _countRespMob++;
                    _timer = 0;
                }
                else if (_countRespMob == _countMobInWave)
                {
                    _timer = 0;
                    _respaunOn = false;
                }
                if (_respaunOn == false)
                {
                    EndWave();
                }
            }
            else
            {
                UpdateWaveConfiguration(_currentWave);
                _timer = 0;
            }
        }
        private void EndWave()
        {
            _arrayMob = _respaun.GetComponentsInChildren<MobManager>();

            if (_arrayMob.Length <= 0)
            {
                _countRespMob = 0;
                _waveManager.CurrentWave++;
                _waveOn = false;
                _waveManager.WaveOn = _waveOn;
            }
        }
        private void UpdateWaveConfiguration(int currentWave)
        {
            _countMobInWave = _waveManager.GetWavesConfiguration._wavesProperties[currentWave - 1].GetCountMobsInWave;
            _countRespaunMobInSec = _waveManager.GetWavesConfiguration._wavesProperties[currentWave - 1].GetCountRespaunMobInSec;
        }
    }
}


