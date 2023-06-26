using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _towerBuildPanel;
        [SerializeField] private GameObject _defeatPanel;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Toggle[] _arrayTowerBuildToggle;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private WaveManager _waveManager;
        private CameraController _cameraController;
        [SerializeField] private bool _pausepanelOn = false;
        [SerializeField] private bool _defeatPanelOn = false;
        [SerializeField] private bool _winPanelOn = false;

        private void Awake()
        {
            _cameraController = new CameraController();
            _cameraController.Controller.EnabledDisabledPauseMenu.performed += ShowPausePanel;
            _arrayTowerBuildToggle = GetComponentsInChildren<Toggle>();
            _gameManager = FindObjectOfType<GameManager>();
            _waveManager = FindObjectOfType<WaveManager>();
        }
        private void OnEnable()
        {
            _cameraController.Controller.Enable();
        }
        private void OnDisable()
        {
            _cameraController.Controller.Disable();
        }
        private void ShowPausePanel(CallbackContext context)
        {
            GameObject[] towersPrimitive = GameObject.FindGameObjectsWithTag("TowerTemplate");
            foreach(GameObject towerPrimitive in towersPrimitive)
            {
                Destroy(towerPrimitive);
            }
            _pausepanelOn = true;
            _pausePanel.SetActive(_pausepanelOn);
            Time.timeScale = 0f;
 
            foreach (var toggle in _arrayTowerBuildToggle)
            {
                toggle.isOn = false;
            }
            _towerBuildPanel.SetActive(false);
        }
        public void HidePausePanel()
        {
            _pausepanelOn = false;
            _pausePanel.SetActive(_pausepanelOn);
            Time.timeScale = 1f;
            _towerBuildPanel.SetActive(true);
        }
        public void ExitMainMenu(int idScene)
        {
            SceneManager.LoadScene(idScene);
            Time.timeScale = 1f;
        }
        public void RestartGame(int idScene)
        {
            Time.timeScale = 1f;
            Destroy(_pausePanel);
            SceneManager.LoadScene(idScene);
        }
        public void OnDefeatPanel()
        {
            _defeatPanelOn = true;
            _defeatPanel.SetActive(_defeatPanelOn);
            foreach (var toggle in _arrayTowerBuildToggle)
            {
                toggle.isOn = false;
            }
            _towerBuildPanel.SetActive(false);
            Time.timeScale = 0f;
        }
        public void OnWinPanel()
        {
            _winPanelOn = true;
            _winPanel.SetActive(_winPanelOn);
            foreach (var toggle in _arrayTowerBuildToggle)
            {
                toggle.isOn = false;
            }
            _towerBuildPanel.SetActive(false);
            Time.timeScale = 0f;
        }
        public void GoToNextLevel(int idScene)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(idScene);
        }    
        private void Update()
        {
            if (_gameManager.CurrentPlayerHealth <= 0 && _defeatPanelOn == false)
            {
                OnDefeatPanel();
            }
            if (_gameManager.CurrentPlayerHealth > 0 && _waveManager.WaveOn == false  && _waveManager.CurrentWave ==_waveManager.GetCountWave)
            {
                OnWinPanel();
            }
        }
    }
}
