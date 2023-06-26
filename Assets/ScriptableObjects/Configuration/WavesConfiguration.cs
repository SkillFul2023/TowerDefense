using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "WavesConfiguration", menuName = "WavesConfig/ Waves Configuration")]
    public class WavesConfiguration : ScriptableObject
    {
        [SerializeField] public WavesProperties[] _wavesProperties;
    }
}
