using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    [Serializable]
    public struct TowerProperties
    {
        [Tooltip("��� �����"),SerializeField] private TypeTower _typeTower;
        [Tooltip("������� �����"), SerializeField] private int _towerLevel;
        [Tooltip("����� ����� �� �������"), SerializeField] private int _attackDamage;
        [Tooltip("�������� ����� ����� (���������/���)"), SerializeField] private int _attackSpeed;
        [Tooltip("��������� ����� �����"), SerializeField] private int _attackRange;
        [Tooltip("����� �� AOE"), SerializeField] private bool _AOEDamage;
        [Tooltip("������ �� �������� �����"), SerializeField] private TowerDamageEffects _towerDamageEffects;
        [Tooltip("��������� ��������� �����"), SerializeField] private int _buildCost;
        [Tooltip("��������� ������� �����"), SerializeField] private int _sellCost;

        public int GetTowerLevel => _towerLevel;
        public int GetTowerAttackDamage => _attackDamage;
        public int GetTowerAttackSpeed => _attackSpeed;
        public int GetTowerAttackRange => _attackRange;
        public bool GetTowerAOE => _AOEDamage;
        public TowerDamageEffects GetTowerAttackEffects => _towerDamageEffects;
        public int GetTowerBuildCost => _buildCost;
        public int GetTowerSellCost => _sellCost;
    }

    [Serializable]
    public struct TypeMob
    {
        [Tooltip("��� ����"), SerializeField] private MobType _mobType;
        [SerializeField] public MobProperties _mobProperties;
        public MobType GetMobType => _mobType;
    }

    [Serializable]
    public struct MobProperties
    {
        [Tooltip("������ ����"), SerializeField] private GameObject _mobModel;
        [Tooltip("��� ����� ����"), SerializeField] private ArmorMobType _armorMobType;
        [Tooltip("���������� ��������"), SerializeField] private float _mobHealth;
        [Tooltip("�������� ������������ ����"), SerializeField] private float _moveSpeedMob;
        [Tooltip("���������� ������ �� �������� ����"), SerializeField] private int _countGoldForMurder;

        public GameObject GetMobModel => _mobModel;
        public ArmorMobType GetArmorMobType => _armorMobType;
        public float GetMobHealth => _mobHealth;
        public float GetMobMoveSpeed => _moveSpeedMob;
        public int GetCountGoldForMurder => _countGoldForMurder;
    }

    [Serializable]
    public struct WavesProperties
    {
        [Tooltip("���������� ����� � �����"), SerializeField] private int _countMobsInWave;
        [Tooltip("�������� �������� ����� (���/���)"), SerializeField] private float _countRespaunMobInSec;
        [Tooltip("����� �����"), SerializeField] private int _waveNumber;
        [Tooltip("��� ����"), SerializeField] private MobType _mobType;
        public int GetCountMobsInWave => _countMobsInWave;
        public float GetCountRespaunMobInSec => _countRespaunMobInSec;
        public MobType GetMobType => _mobType;
    }
}
