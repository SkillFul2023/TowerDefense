using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    [Serializable]
    public struct TowerProperties
    {
        [Tooltip("Тип башни"),SerializeField] private TypeTower _typeTower;
        [Tooltip("Уровень башни"), SerializeField] private int _towerLevel;
        [Tooltip("Урона башни за выстрел"), SerializeField] private int _attackDamage;
        [Tooltip("Скорость атаки башни (выстрелов/мин)"), SerializeField] private int _attackSpeed;
        [Tooltip("Дальность атаки башни"), SerializeField] private int _attackRange;
        [Tooltip("Атака по AOE"), SerializeField] private bool _AOEDamage;
        [Tooltip("Эффект от выстрела башни"), SerializeField] private TowerDamageEffects _towerDamageEffects;
        [Tooltip("Стоимость постройки башни"), SerializeField] private int _buildCost;
        [Tooltip("Стоимость продажи башни"), SerializeField] private int _sellCost;

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
        [Tooltip("Тип моба"), SerializeField] private MobType _mobType;
        [SerializeField] public MobProperties _mobProperties;
        public MobType GetMobType => _mobType;
    }

    [Serializable]
    public struct MobProperties
    {
        [Tooltip("Модель моба"), SerializeField] private GameObject _mobModel;
        [Tooltip("Тип брони моба"), SerializeField] private ArmorMobType _armorMobType;
        [Tooltip("Количество здоровья"), SerializeField] private float _mobHealth;
        [Tooltip("Скорость передвижения моба"), SerializeField] private float _moveSpeedMob;
        [Tooltip("Количество золота за убийство моба"), SerializeField] private int _countGoldForMurder;

        public GameObject GetMobModel => _mobModel;
        public ArmorMobType GetArmorMobType => _armorMobType;
        public float GetMobHealth => _mobHealth;
        public float GetMobMoveSpeed => _moveSpeedMob;
        public int GetCountGoldForMurder => _countGoldForMurder;
    }

    [Serializable]
    public struct WavesProperties
    {
        [Tooltip("Количество мобов в волне"), SerializeField] private int _countMobsInWave;
        [Tooltip("Скорость респауна мобов (моб/сек)"), SerializeField] private float _countRespaunMobInSec;
        [Tooltip("Номер волны"), SerializeField] private int _waveNumber;
        [Tooltip("Тип моба"), SerializeField] private MobType _mobType;
        public int GetCountMobsInWave => _countMobsInWave;
        public float GetCountRespaunMobInSec => _countRespaunMobInSec;
        public MobType GetMobType => _mobType;
    }
}
