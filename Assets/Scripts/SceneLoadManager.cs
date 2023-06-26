#if UNITY_EDITOR
#define DEFINE_ONE
#else
#define DEFINE_TWO
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.IO;
using Newtonsoft.Json;

namespace TowerDefense
{
    public class SceneLoadManager : MonoBehaviour
    {
        [SerializeField] public static bool _loadSaveGame = false;
        [SerializeField] private GameObject _loadButton;
        [SerializeField] private int _idLoadScene;
        [SerializeField] public IDSceneOptions _idSceneOptions;

        [Serializable]
        public class IDSceneOptions
        {
            public int IdLoadScene;
        }

        private void Awake()
        {
            _idSceneOptions = new IDSceneOptions();

            var currDirct = Environment.CurrentDirectory;
#if DEFINE_ONE

            string saveFile = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
#else
            string saveFile = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/SaveGameOptions.json";
#endif
            if (File.Exists(saveFile))
            {
                _loadButton.SetActive(true);
            }
            else
            {
                _loadButton.SetActive(false);
                _idLoadScene = 0;
            }
        }
        public void ChangeScene(int idScene)
        {
            _loadSaveGame = false;
            SceneManager.LoadScene(idScene);
        }
        public void ExitGame()
        {
            #if DEFINE_ONE
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
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

            string idSceneOption = currDirct + "/Assets/" + "/StreamingAssets/" + "/Save/" + "/IDScene.json";
#else
            string idSceneOption = currDirct + "/TowerDefense_Data/" + "/StreamingAssets/" + "/Save/" + "/IDScene.json";
#endif
            _idSceneOptions = JsonConvert.DeserializeObject<IDSceneOptions>(File.ReadAllText(idSceneOption), settings);
            _idLoadScene = _idSceneOptions.IdLoadScene;
            _loadSaveGame = true;
            SceneManager.LoadScene(_idLoadScene);
        }
    }
}
