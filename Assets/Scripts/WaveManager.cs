using UnityEngine;

namespace TowerDefense
{

    public class WaveManager : MonoBehaviour
    {
        [Tooltip("Текущая волна"), SerializeField] private int _currentWave = 1;
        [Tooltip("общее количестов волн"), SerializeField] private int _countWave;
        [Tooltip("Время между волнами"), SerializeField] private float _timeBetweenWave;
        [SerializeField] private bool _waveOn = false;
        [SerializeField] private WavesConfiguration _wavesConfiguration;
        [SerializeField] private MobConfiguration _mobConfiguration;
        [SerializeField, Space] private MobType _mobType;
        [SerializeField] private GameObject _mob;
        [SerializeField] private ArmorMobType _armorMobType;
        [SerializeField] private float _mobHealth;
        [SerializeField] private float _mobMoveSpeed;
        [SerializeField] private int _countGoldForMurder;
        [SerializeField] private float _timer;

        public WavesConfiguration GetWavesConfiguration => _wavesConfiguration;
        public MobConfiguration GetMobConfiguration => _mobConfiguration;
        public bool WaveOn
        {
            get => _waveOn;
            set => _waveOn = value;
        }
        public int CurrentWave
        {
            get => _currentWave;
            set => _currentWave = value;
        }
        public int GetCountWave => _countWave;
        public GameObject GetMobModel => _mob;
        public ArmorMobType GetArmorMobType => _armorMobType;
        public float GetMobHealth => _mobHealth;
        public float GetMobMoveSpeed => _mobMoveSpeed;
        public int GetCountGoldForMurder => _countGoldForMurder;
        public float GetTimer => _timer;
        private void Awake()
        {
            _timer = _timeBetweenWave;
            _countWave = _wavesConfiguration._wavesProperties.Length;
            UpdateMobConfiguration(_currentWave);
        }
        private void UpdateMobConfiguration(int currentWave)
        {
            _mobType = GetWavesConfiguration._wavesProperties[currentWave - 1].GetMobType;
            int i = 0;

            foreach (var mob in GetMobConfiguration._typeMob)
            {
                if (mob.GetMobType == _mobType)
                {
                    i++;
                    break;
                }
                else
                {
                    i++;
                }
            }
            _mob = GetMobConfiguration._typeMob[i - 1]._mobProperties.GetMobModel;
            _armorMobType = GetMobConfiguration._typeMob[i - 1]._mobProperties.GetArmorMobType;
            _mobHealth = GetMobConfiguration._typeMob[i - 1]._mobProperties.GetMobHealth;
            _mobMoveSpeed = GetMobConfiguration._typeMob[i - 1]._mobProperties.GetMobMoveSpeed;
            _countGoldForMurder = GetMobConfiguration._typeMob[i - 1]._mobProperties.GetCountGoldForMurder;
        }
        private void Update()
        {
            if (_waveOn == false)
            {
                UpdateMobConfiguration(_currentWave);
                _timer = _timer - Time.deltaTime;
                
                if(_timer <= 0)
                {
                    _waveOn = true;
                    _timer = _timeBetweenWave;
                }
            }
        }
    }
}


