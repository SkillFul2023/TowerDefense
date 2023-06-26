using UnityEngine;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class MobManager : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private ArmorMobType _armorMobType;
        [SerializeField] private float _maxhealth;
        [SerializeField] private int _goldForMurder;
        [SerializeField, Space] private float _mobMoveSpeed;
        [SerializeField] private bool _mobSlowMove;
        [SerializeField] private int _slowCoeff;
        [SerializeField] private float _mobMoveSpeedAfterSlowEffect;
        [SerializeField] private float _timeForSlowMob;
        [SerializeField, Space] private bool _mobPoisonEffect;
        [SerializeField] private float _timeToPoisonMob;
        [SerializeField] private float _poisonDamage;
        [SerializeField, Space] private GameManager _gameManager;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private AnimationMob _animationMob;
        [SerializeField] private StateType _stateType;
        [SerializeField] private Image _currentMobHealth;
        [SerializeField] private GameObject _deathObject;
        [SerializeField] private GameObject _healthBar;
        private float _currentMoveSpeedMob;
        private bool _getGold = false;
        private Camera _camera;
        private Collider _collider;

        public event EventHandler OnEndAnimation;

        public float HealthMob
        {
            get => _health;
            set => _health = value;
        }
        public float SetMobMoveSpeed
        {
            set => _mobMoveSpeed = value;
        }
        public bool SetMobSlowMove
        {
            set => _mobSlowMove = value;
        }
        public float SetTimeForSlowMob
        {
            set => _timeForSlowMob = value;
        }
        public float GetCurrentBotMoveSpeed => _currentMoveSpeedMob;
        public bool SetPoisonEffectOnMob
        {
            set => _mobPoisonEffect = value;
        }
        public float SetTimeToPoison
        {
            set => _timeToPoisonMob = value;
        }
        public float SetPoisonDamage
        {
            set => _poisonDamage = value;
        }
        public ArmorMobType GetArmorMobType => _armorMobType;
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _waveManager = FindObjectOfType<WaveManager>();
            _animationMob = GetComponentInChildren<AnimationMob>();
            _health = _waveManager.GetMobHealth;
            _armorMobType = _waveManager.GetArmorMobType;
            _maxhealth = _health;
            _goldForMurder = _waveManager.GetCountGoldForMurder;
            _mobMoveSpeed = _waveManager.GetMobMoveSpeed;
            _slowCoeff = _gameManager.GetSlowCoefficient;
            _mobMoveSpeedAfterSlowEffect = _mobMoveSpeed - _mobMoveSpeed * (_slowCoeff / 100f);
            _camera = Camera.main;
            _collider = GetComponent<Collider>();
        }
        private void Update()
        {
            _stateType = _animationMob.GetStateType;

            if (_stateType == StateType.Death)
            {
                Destroy(_collider);
                _healthBar.SetActive(false);
                _deathObject.SetActive(true);

                if (_getGold == false)
                {
                    _gameManager.CurrentPlayerGold += _goldForMurder;
                    _getGold = true;
                    Debug.Log("Моб убит, получено " + _goldForMurder + " золота");
                }
            }

            if (_mobSlowMove == true)
            {
                if (_stateType == StateType.Death)
                {
                    _currentMoveSpeedMob = 0;
                }
                else
                {
                    _currentMoveSpeedMob = _mobMoveSpeedAfterSlowEffect;
                }
                _timeForSlowMob -= Time.deltaTime;
                
                if(_timeForSlowMob <= 0)
                {
                    _mobSlowMove = false;
                }
            }
            else
            {
                _currentMoveSpeedMob = _mobMoveSpeed;
            }
            if(_mobPoisonEffect == true)
            {
                MobHealthDown();
            }

            UpdateMobHealthFoImg(_health);
        }
        private void LateUpdate()
        {
            _healthBar.transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
            //transform.Rotate(0, 180, 0);
        }
        private void UpdateMobHealthFoImg(float currentMobHealth)
        {
            _currentMobHealth.fillAmount = currentMobHealth / _maxhealth;
        }

        private void MobHealthDown()
        {
            _timeToPoisonMob -= Time.deltaTime;
            if(_timeToPoisonMob <= 0)
            {
                _mobPoisonEffect = false;
            }
            _health -= _poisonDamage * Time.deltaTime;
        }
    }
}

