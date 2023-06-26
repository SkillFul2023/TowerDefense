using UnityEngine;

namespace TowerDefense
{
    public class MobMazeMove : MonoBehaviour
    {
        [SerializeField] private GameObject[] _movePoint;
        [SerializeField] private MazeWay _mazeWay;
        [SerializeField] private GameObject _targetForMove;
        [SerializeField] private float _moveMobSpeed;
        [SerializeField] private int _indexTarget;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            _mazeWay = FindObjectOfType<MazeWay>();
            _gameManager = FindObjectOfType<GameManager>();
            _movePoint = _mazeWay.mazeWay;
            _indexTarget = 0;
            _targetForMove = _movePoint[_indexTarget];
        }

        public void MobMoving()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetForMove.transform.position, _moveMobSpeed * Time.deltaTime);
             
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "MovePoint")
            {
                _indexTarget++;
                _targetForMove = _movePoint[_indexTarget];
                transform.LookAt(_targetForMove.transform.position);
            }
            else if (collider.gameObject.tag == "Main")
            {
                Destroy(gameObject);
                _gameManager.CurrentPlayerHealth -= 1;

            }
        }
        private void Update()
        {
            _moveMobSpeed = GetComponent<MobManager>().GetCurrentBotMoveSpeed;
            MobMoving();
        }
    }
    
}

