using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace TowerDefense
{
    public class TowerManager : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TypeTower _typeTower;
        [SerializeField] private int _towerLevel;
        [SerializeField] private int _attackDamage;
        [SerializeField] private int _attackSpeed;
        [SerializeField] private int _attackRange;
        [SerializeField] private bool _AOEDamage;
        [SerializeField] private float _AOEAreaDamage;
        [SerializeField] private float _AOEAreaEffect;
        [SerializeField] private TowerDamageEffects _towerDamageEffects;
        [SerializeField] private int _buildCost;
        [SerializeField] private int _sellCost;
        [SerializeField, Range(0.1f, 10)] private float _mooveSpeedProjectile;
        [SerializeField] public TowersConfiguration _towersConfiguration;
        [Space] public GameObject _towerInterface;
        [SerializeField] private int _currentTowerLvl;
        public bool _towerInterfaceOn = false;
        [SerializeField] private UIManager _uIManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _upgradeTowerObject;
        [SerializeField] private TMP_Text _levelTowerText;
        [SerializeField] private GameObject _canTowerUpgradeObject;
        [SerializeField] private BuildManager _buildManager;
        [SerializeField] private GameObject _buildPlace;
        [SerializeField] private GameObject _upgradeEffect;
        private Camera _camera;

        public int GetBuildCost => _buildCost;
        public int GetAttackRange => _attackRange;
        public int GetAttackSpeed => _attackSpeed;
        public float GetMooveSpeedProjectile => _mooveSpeedProjectile;
        public int GetAttackDamage => _attackDamage;
        public bool GetAOEDamage => _AOEDamage;
        public float GetAOEAreaDamage => _AOEAreaDamage;
        public float GetAOEAreaEffect => _AOEAreaEffect;
        public TypeTower GetTowerType => _typeTower;

        public int SetTowerLevel
        {
            set => _currentTowerLvl = value;
        }
        private void Awake()
        {
            _uIManager = FindObjectOfType<UIManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _towerInterface = transform.Find("TowerInterface").gameObject;
            _camera = Camera.main;
        }
        private void Start()
        {
            _buildPlace = transform.parent.gameObject;
            _buildManager = _buildPlace.GetComponent<BuildManager>();
            UpdateTowerProperties(_currentTowerLvl);
        }
        private void Update()
        {
            if(_towerLevel >= 5f)
            {
                _upgradeTowerObject.SetActive(false);
                _canTowerUpgradeObject.SetActive(false);
            }
            else
            {
                if(_gameManager.CurrentPlayerGold >= _towersConfiguration._towerProperties[_towerLevel].GetTowerBuildCost)
                {
                    _canTowerUpgradeObject.SetActive(true);
                }
                else
                {
                    _canTowerUpgradeObject.SetActive(false);
                }
            }
        }
        private void LateUpdate()
        {
            _towerInterface.transform.LookAt(new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        }
        public IEnumerator UpgradeTower()
        {
            int costUpTower = GetComponent<TowerManager>()._towersConfiguration._towerProperties[_currentTowerLvl].GetTowerBuildCost;

            if (costUpTower <= _gameManager.CurrentPlayerGold)
            {
                if (_currentTowerLvl > 1)
                {
                    _upgradeEffect.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    _upgradeEffect.SetActive(true);
                }
                _currentTowerLvl++;
                UpdateTowerProperties(_currentTowerLvl);
                _gameManager.CurrentPlayerGold -= _buildCost;
                Debug.Log(gameObject.name + " улучшена до " + _towerLevel + " уровня");
            }
            else
            {
                string needGold = (costUpTower - _gameManager.CurrentPlayerGold).ToString();
                Debug.Log("Не хватает " + needGold + " золота для улучшения " + gameObject.name + " до "+ (_currentTowerLvl+1) + " уровня");
            }
            yield return null;
        }
        public IEnumerator DestroTower()
        {
            Destroy(gameObject);
            _gameManager.CurrentPlayerGold += _sellCost;
            _buildManager.CanBuild = true;
            yield return null;
        }

        public void OnDestroyTower_EditorEvent()
        {
            StartCoroutine(DestroTower());
        }
        public void OnUpgradeTower_EditorEvent()
        {
            StartCoroutine(UpgradeTower());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_uIManager.GetBuildTowerOn == false)
            {
                _towerInterfaceOn = !_towerInterfaceOn;
                _towerInterface.gameObject.SetActive(_towerInterfaceOn);
                if (_towerInterfaceOn == true)
                {
                    _uIManager.SelectTowerForInfo = gameObject;
                }
                else
                {
                    _uIManager.SelectTowerForInfo = null;
                }
                
            }
        }
        public void UpdateTowerProperties(int towerLevel)
        {
            towerLevel = _currentTowerLvl - 1;
            _towerLevel = _towersConfiguration._towerProperties[towerLevel].GetTowerLevel;
            _attackDamage = _towersConfiguration._towerProperties[towerLevel].GetTowerAttackDamage;
            _attackSpeed = _towersConfiguration._towerProperties[towerLevel].GetTowerAttackSpeed;
            _attackRange = _towersConfiguration._towerProperties[towerLevel].GetTowerAttackRange;
            _AOEDamage = _towersConfiguration._towerProperties[towerLevel].GetTowerAOE;
            _towerDamageEffects = _towersConfiguration._towerProperties[towerLevel].GetTowerAttackEffects;
            _buildCost = _towersConfiguration._towerProperties[towerLevel].GetTowerBuildCost;
            _sellCost = _towersConfiguration._towerProperties[towerLevel].GetTowerSellCost;
            _levelTowerText.text = _towerLevel.ToString();
            _buildManager.TowerLevel = _towerLevel;
            _buildManager.TypeTower = _typeTower;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Gizmos.DrawWireSphere(position, _attackRange);
        }
    }
}
