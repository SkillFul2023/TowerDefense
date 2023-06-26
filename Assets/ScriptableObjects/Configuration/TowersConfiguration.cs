using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "NewTowersConfiguration", menuName = "TowersConfig/ Towers Configuration")]
    public class TowersConfiguration : ScriptableObject
    {
        [SerializeField] public TowerProperties[] _towerProperties;
    }
}
