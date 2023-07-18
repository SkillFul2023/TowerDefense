using UnityEngine;

namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        [Tooltip("������� ���������� ������"), SerializeField] private int _playerCurrentGold;
        [Tooltip("��������� ���������� ��������"), SerializeField] private int _playerCountHealth;
        [Tooltip("% ���������� ����"), SerializeField, Range(0,100)] private int _slowCoefficient;
        [Tooltip("����� ���������� ���� �� �������"), SerializeField, Range(0, 2)] private float _timeToSlowMob;
        [Tooltip("����� ��������� ������ ����� �� ����������"), SerializeField, Range(0, 3)] private float _timeToPoisonMob;
        [Tooltip("E��� �� ���������� � ���"), SerializeField, Range(0, 10)] private float _poisonDamage;
        public int CurrentPlayerGold
        {
            get => _playerCurrentGold;
            set => _playerCurrentGold = value;
        }
        public int CurrentPlayerHealth
        {
            get => _playerCountHealth;
            set => _playerCountHealth = value;
        }
        public int GetSlowCoefficient => _slowCoefficient;
        public float GetTimeToSlowMob => _timeToSlowMob;
        public float GetTimeToPoisonMob => _timeToPoisonMob;
        public float GetPoisonDamage => _poisonDamage;

        private void Awake()
        {
            AssignIDForBuildPlace();
        }
        public void AssignIDForBuildPlace()
        {
            GameObject[] buildPlace = GameObject.FindGameObjectsWithTag("BuildPalce");

            for (int i = 0; i < buildPlace.Length; i++)
            {
                buildPlace[i].GetComponent<BuildManager>().IDBuildPlace = i;
            }
        }
    }
}

