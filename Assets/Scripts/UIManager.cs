using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _towerTemplate_1;
        [SerializeField] private GameObject _towerTemplate_2;
        [SerializeField] private GameObject _towerTemplate_3;
        [SerializeField] private GameObject _towerTemplate_4;
        [SerializeField] private GameObject _towerTemplate_5;
        [Space, SerializeField] private GameObject _originalTower_1;
        [SerializeField] private GameObject _originalTower_2;
        [SerializeField] private GameObject _originalTower_3;
        [SerializeField] private GameObject _originalTower_4;
        [SerializeField] private GameObject _originalTower_5;
        [Space, SerializeField] private GameObject _selectTowerTemplate;
        [SerializeField] private GameObject _selectTowerForBuild;
        [SerializeField] private GameObject _selectTowerForInfo;
        [SerializeField] private bool _buildTowerOn;
        [SerializeField] private GameObject[] _checkTowers;
        [Space, SerializeField] public GameManager _gameManager;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private TMP_Text _playerCurrentGoldText;
        [SerializeField] private TMP_Text _playerCountHealthText;
        [SerializeField] private TMP_Text _currentWaveText;
        [SerializeField] private TMP_Text _countWaveText;
        [SerializeField] private GameObject _timerInformPanel;
        [SerializeField] private TMP_Text _timerForNextWave;
        [SerializeField] private Image _healthPlayerImg;
        private float _playerStartHealth;
        [SerializeField] private TMP_Text _costTower_1;
        [SerializeField] private TMP_Text _costTower_2;
        [SerializeField] private TMP_Text _costTower_3;
        [SerializeField] private TMP_Text _costTower_4;
        [SerializeField] private TMP_Text _costTower_5;
        [SerializeField] private GameObject _towerInfoPanel;

        public bool GetBuildTowerOn
        {
            get => _buildTowerOn;
        }
        public GameObject GetSelectTowerTemplate
        {
            get => _selectTowerTemplate;
        }
        public GameObject GetSelectTowerForBuild
        {
            get => _selectTowerForBuild;
        }

        public GameObject SelectTowerForInfo
        {
            get => _selectTowerForInfo;
            set => _selectTowerForInfo = value;
        }

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _waveManager = FindObjectOfType<WaveManager>();
            _towerTemplate_1 = Resources.Load<GameObject>("Prefabs/Tower/ArrowTowerModelTemplate");
            _towerTemplate_2 = Resources.Load<GameObject>("Prefabs/Tower/SiegeTowerModelTemplate");
            _towerTemplate_3 = Resources.Load<GameObject>("Prefabs/Tower/IceTowerModelTemplate");
            _towerTemplate_4 = Resources.Load<GameObject>("Prefabs/Tower/PoisonTowerModelTemplate");
            _towerTemplate_5 = Resources.Load<GameObject>("Prefabs/Tower/ChaosTowerModelTemplate");
            _originalTower_1 = Resources.Load<GameObject>("Prefabs/Tower/ArrowTowerModel");
            _originalTower_2 = Resources.Load<GameObject>("Prefabs/Tower/SiegeTowerModel");
            _originalTower_3 = Resources.Load<GameObject>("Prefabs/Tower/IceTowerModel");
            _originalTower_4 = Resources.Load<GameObject>("Prefabs/Tower/PoisonTowerModel");
            _originalTower_5 = Resources.Load<GameObject>("Prefabs/Tower/ChaosTowerModel");
            _playerStartHealth = _gameManager.CurrentPlayerHealth;
            _buildTowerOn = false;
            _costTower_1.text = _originalTower_1.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost.ToString();
            _costTower_2.text = _originalTower_2.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost.ToString();
            _costTower_3.text = _originalTower_3.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost.ToString();
            _costTower_4.text = _originalTower_4.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost.ToString();
            _costTower_5.text = _originalTower_5.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost.ToString();
        }
        private void Update()
        {
            UpdatePlayerStats();

            if (_selectTowerForInfo != null)
            {
                _towerInfoPanel.SetActive(true);
            }
            else
            {
                _towerInfoPanel.SetActive(false);
            }
        }
        public void OnCreateArrowTowerForBuild_EditorEvent()
        {
            if (_checkTowers[0].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("TRUEArrow");
                OffAllToggle(0);
                _selectTowerTemplate = _towerTemplate_1;
                _selectTowerForBuild = _originalTower_1;
            }
            else
            {
                Debug.Log("FALSEArrow");
            }
        }
        public void OnCreateSiegeTowerForBuild_EditorEvent()
        {
            if (_checkTowers[1].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("TRUESiege");
                OffAllToggle(1);
                _selectTowerTemplate = _towerTemplate_2;
                _selectTowerForBuild = _originalTower_2;
            }
            else
            {
                Debug.Log("FALSESiege");
            }
        }
        public void OnCreateIceTowerForBuild_EditorEvent()
        {
            if (_checkTowers[2].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("TRUEIce");
                OffAllToggle(2);
                _selectTowerTemplate = _towerTemplate_3;
                _selectTowerForBuild = _originalTower_3;
            }
            else
            {
                Debug.Log("FALSEIce");
            }
        }
        public void OnCreatePoisonTowerForBuild_EditorEvent()
        {
            if (_checkTowers[3].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("TRUEPoison");
                OffAllToggle(3);
                _selectTowerTemplate = _towerTemplate_4;
                _selectTowerForBuild = _originalTower_4;
            }
            else
            {
                Debug.Log("FALSEPoison");
            }
        }
        public void OnCreateChaosTowerForBuild_EditorEvent()
        {
            if (_checkTowers[4].GetComponent<Toggle>().isOn == true)
            {
                Debug.Log("TRUEChaos");
                OffAllToggle(4);
                _selectTowerTemplate = _towerTemplate_5;
                _selectTowerForBuild = _originalTower_5;
            }
            else
            {
                Debug.Log("FALSEChaos");
            }
        }
        #region Все isOn переводим в false кроме нажатого CheckBox, для выбора только одного шаблона башни для строительства
        public void OffAllToggle(int towerID)
        {
            for (int i = 0; i < _checkTowers.Length; i++)
            {
                if (i == towerID)
                {
                    continue;
                }
                _checkTowers[i].GetComponent<Toggle>().isOn = false;
            }
        }
        #endregion
        #region Проверка возможности предварительного размещения шаблона башни
        public void OnCheckForCanBuild_EditorEvent()
        {
            foreach(GameObject tower in _checkTowers)
            {
                if (tower.GetComponent<Toggle>().isOn == true)
                {
                    _buildTowerOn = true;
                    break;
                }
                else
                {
                    _buildTowerOn = false;
                }
            }
        }
        #endregion
        public void UpdatePlayerStats()
        {
            bool onInfoPanel = _waveManager.WaveOn;
            float timerForNExtWave = _waveManager.GetTimer;

            _playerCurrentGoldText.text = _gameManager.CurrentPlayerGold.ToString();
            _playerCountHealthText.text = _gameManager.CurrentPlayerHealth.ToString();
            _currentWaveText.text = _waveManager.CurrentWave.ToString();
            _countWaveText.text = _waveManager.GetCountWave.ToString();
            _timerForNextWave.text = ((int)timerForNExtWave).ToString();
            _timerInformPanel.SetActive(!onInfoPanel);
            _healthPlayerImg.fillAmount = (float)_gameManager.CurrentPlayerHealth / _playerStartHealth;
        }
    }
}

