using UnityEngine;
namespace TowerDefense
{
    public class AnimationMob : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private StateType _stateType;
        [SerializeField] private MobManager _mobManager;
        [SerializeField] private float _health;

        public StateType GetStateType => _stateType;
        private void Awake()
        {
            _mobManager = GetComponentInParent<MobManager>();
            _stateType = StateType.Move;
        }
        private void Update()
        {
            _health = _mobManager.HealthMob;
            if (_health <= 0)
            {
                _stateType = StateType.Death;
                StartAnimation("Death");
                _mobManager.SetMobMoveSpeed = 0f;
            }
        }
        public void StartAnimation(string animation)
        {
            _animator.SetTrigger(animation);
        }
    }
    
}

