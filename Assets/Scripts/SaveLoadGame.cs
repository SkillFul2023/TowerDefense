#if UNITY_EDITOR
#define DEFINE_ONE
#else
#define DEFINE_TWO
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class SaveLoadGame : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] public GameOptions _gameOptions;
        [SerializeField] public List<GameObject> _buildPlace;
        [SerializeField] public List<TowerOptions> _towerOptions;
        [SerializeField] public IDSceneOptions _idSceneOptions;
        [SerializeField] private GameObject _originalTower_1;
        [SerializeField] private GameObject _originalTower_2;
        [SerializeField] private GameObject _originalTower_3;
        [SerializeField] private GameObject _originalTower_4;
        [SerializeField] private GameObject _originalTower_5;
        [SerializeField, Space] private bool _loadSaveGame;
        const float _hightTower = 1;

        [Serializable]
        public class GameOptions
        {
            public int CoutGold;
            public int CurrentWave;
            public int CurrentHealth;
        }
        [Serializable]
        public class TowerOptions
        {
            public TypeTower TypeTower;
            public int TowerLevel;
            public int IdBuildPlace;

            public TowerOptions(TypeTower typeTower, int towerLevel, int idBuildPlace)
            {
                TypeTower = typeTower;
                TowerLevel = towerLevel;
                IdBuildPlace = idBuildPlace;
            }
        }
        public class IDSceneOptions
        {
            public int IdLoadScene;
        }
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _waveManager = FindObjectOfType<WaveManager>();
            _gameOptions = new GameOptions();
            _towerOptions = new List<TowerOptions>();
            _idSceneOptions = new IDSceneOptions();
            _loadSaveGame = SceneLoadManager._loadSaveGame;

            if(_loadSaveGame == true)
            {
                LoadGame();
            }
        }
        public void SaveGame()
        {
            _buildPlace.Clear();
            _towerOptions.Clear();

            _gameOptions.CoutGold = _gameManager.CurrentPlayerGold;
            _gameOptions.CurrentWave = _waveManager.CurrentWave;
            _gameOptions.CurrentHealth = _gameManager.CurrentPlayerHealth;
            _idSceneOptions.IdLoadScene = SceneManager.GetActiveScene().buildIndex;

            GameObject[] buildPlace = GameObject.FindGameObjectsWithTag("BuildPalce");
            foreach(GameObject bp in buildPlace)
            {
                if(bp.GetComponent<BuildManager>().CanBuild == false)
                {
                    _buildPlace.Add(bp);
                    var buildManager = bp.GetComponent<BuildManager>();
                    _towerOptions.Add(new TowerOptions(buildManager.TypeTower, buildManager.TowerLevel, buildManager.IDBuildPlace));
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None
            };
            var currDirct = Environment.CurrentDirectory;
#if DEFINE_ONE

            string fileGameOptions = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
            string towerOptions = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/TowerOptions.json";
            string idSceneOption = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/IDScene.json";
#else
            string fileGameOptions = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
            string towerOptions = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/TowerOptions.json";
            string idSceneOption = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/IDScene.json";
#endif
            File.WriteAllText(fileGameOptions, JsonConvert.SerializeObject(_gameOptions, settings));
            File.WriteAllText(towerOptions, JsonConvert.SerializeObject(_towerOptions, settings));
            File.WriteAllText(idSceneOption, JsonConvert.SerializeObject(_idSceneOptions, settings));

            string str = string.Empty;
            using (StreamReader reader = File.OpenText(idSceneOption))
            {
                str = reader.ReadToEnd();
            }

            str = str.Replace("SaveLoadGame", "SceneLoadManager");

            using (StreamWriter file = new StreamWriter(idSceneOption))
            {
                file.Write(str);
            }
                //File.OpenText(idSceneOption).ReadToEnd().Replace("SaveLoadGame", "SceneLoadManager");
            Debug.Log("Save");
        }
        public void LoadGame()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            var currDirct = Environment.CurrentDirectory;
#if DEFINE_ONE

            string fileGameOptions = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
            string towerOptions = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/TowerOptions.json";
#else
            string fileGameOptions = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
            string towerOptions = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/TowerOptions.json";
#endif
            _gameOptions = JsonConvert.DeserializeObject<GameOptions>(File.ReadAllText(fileGameOptions), settings);
            _towerOptions = JsonConvert.DeserializeObject<List<TowerOptions>>(File.ReadAllText(towerOptions), settings);

            _gameManager.CurrentPlayerGold = _gameOptions.CoutGold;
            _gameManager.CurrentPlayerHealth = _gameOptions.CurrentHealth;
            _waveManager.CurrentWave = _gameOptions.CurrentWave;

            LoadTowerOnBuildPlace();
            
        }
        public void LoadTowerOnBuildPlace()
        {
            GameObject[] buildPlace = GameObject.FindGameObjectsWithTag("BuildPalce");
            foreach (GameObject bp in buildPlace)
            {
                for (int i = 0; i < _towerOptions.Count; i++)
                {
                    if(bp.GetComponent<BuildManager>().IDBuildPlace == _towerOptions[i].IdBuildPlace)
                    {
                        _buildPlace.Add(bp);
                        if (_towerOptions[i].TypeTower == TypeTower.ArrowTower)
                        {
                            StartCoroutine(BuildLoadTower(bp, _originalTower_1, _towerOptions[i].TowerLevel));
                        }
                        else if (_towerOptions[i].TypeTower == TypeTower.SiegeTower)
                        {
                            StartCoroutine(BuildLoadTower(bp, _originalTower_2, _towerOptions[i].TowerLevel));
                        }
                        else if (_towerOptions[i].TypeTower == TypeTower.IceTower)
                        {
                            StartCoroutine(BuildLoadTower(bp, _originalTower_3, _towerOptions[i].TowerLevel));
                        }
                        else if (_towerOptions[i].TypeTower == TypeTower.PoisonTower)
                        {
                            StartCoroutine(BuildLoadTower(bp, _originalTower_4, _towerOptions[i].TowerLevel));
                        }
                        else if (_towerOptions[i].TypeTower == TypeTower.ChaosTower)
                        {
                            StartCoroutine(BuildLoadTower(bp, _originalTower_5, _towerOptions[i].TowerLevel));
                        }

                    }
                }
            }
        }
        public IEnumerator BuildLoadTower(GameObject buildPlace, GameObject towerModel, int towerLevel)
        {
            Vector3 position = new Vector3(buildPlace.transform.position.x, _hightTower, buildPlace.transform.position.z);
            var obj = Instantiate(towerModel, position, transform.rotation);
            obj.transform.parent = buildPlace.gameObject.transform;
            buildPlace.GetComponent<BuildManager>().CanBuild = false;
            obj.GetComponent<TowerManager>().SetTowerLevel = towerLevel;
            yield return null;
        }
    }
}
