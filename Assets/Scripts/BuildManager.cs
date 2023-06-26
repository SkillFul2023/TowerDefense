using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private bool _canBuild = true;
        const float _hightTower = 1;
        [SerializeField] public GameObject _towerTemplate;
        [SerializeField] private TypeTower _typeTower;
        [SerializeField] private int _towerLevel;
        [SerializeField] private int _idBuildPlace;

        public bool CanBuild
        {
            get => _canBuild;
            set => _canBuild = value;
        }
        public TypeTower TypeTower
        {
            get => _typeTower;
            set => _typeTower = value;
        }
        public int TowerLevel
        {
            get => _towerLevel;
            set => _towerLevel = value;
        }
        public int IDBuildPlace
        {
            get => _idBuildPlace;
            set => _idBuildPlace = value;
        }

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _gameManager = FindObjectOfType<GameManager>();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_canBuild == true && _uiManager.GetBuildTowerOn == true)
            {
                GameObject selectTower = _uiManager.GetSelectTowerTemplate;
                StartCoroutine(CreateTowerTemplate(selectTower));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_canBuild == true && _uiManager.GetBuildTowerOn == true)
            {
                GameObject towerForDestroy = this.gameObject.transform.GetChild(0).gameObject;
                StartCoroutine(DestroyTowerTemplate(towerForDestroy));
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_canBuild == true && _uiManager.GetBuildTowerOn == true)
            {
                StartCoroutine(BuildTower(_uiManager.GetSelectTowerForBuild));
            }
        }
        public IEnumerator CreateTowerTemplate(GameObject gameObject)
        {
            Vector3 position = new Vector3(transform.position.x, _hightTower, transform.position.z);
            var obj = Instantiate(gameObject, position, transform.rotation);
            obj.transform.parent = this.gameObject.transform;
            yield return null;
        }
        public IEnumerator DestroyTowerTemplate(GameObject gameObject)
        {
            Destroy(gameObject);
            yield return null;
        }
        
        public IEnumerator BuildTower(GameObject gameObject)
        {
            int towerBuidCost = gameObject.GetComponent<TowerManager>()._towersConfiguration._towerProperties[0].GetTowerBuildCost;
            if (towerBuidCost <= _gameManager.CurrentPlayerGold)
            {
                Vector3 position = new Vector3(transform.position.x, _hightTower, transform.position.z);
                var obj = Instantiate(gameObject, position, transform.rotation);
                obj.transform.parent = this.gameObject.transform;
                _gameManager.CurrentPlayerGold -= towerBuidCost;

                GameObject towerForDestroy = this.gameObject.transform.GetChild(0).gameObject;
                StartCoroutine(DestroyTowerTemplate(towerForDestroy));

                _canBuild = false;
            }
            else
            {
                string needGold = (towerBuidCost - _gameManager.CurrentPlayerGold).ToString(); 
                Debug.Log("Не хватает " + needGold + " золота для посторйки " + gameObject.name);
            }
            yield return null;
        }
    }
}
