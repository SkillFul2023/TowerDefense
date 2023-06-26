using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "MobConfiguration", menuName = "MobConfig/ Mob Configuration")]
    public class MobConfiguration : ScriptableObject
    {
        [SerializeField] public TypeMob[] _typeMob;
    }
}
