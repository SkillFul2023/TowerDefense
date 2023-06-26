using UnityEngine;

namespace TowerDefense
{
    public class DestroyMobModel : MonoBehaviour
    {
        [SerializeField] private MobManager _mobManager;
        [SerializeField] private AnimationMob _animationMob;
        [SerializeField] private StateType _stateType;
        [SerializeField] float _timer = 3;
        private void Awake()
        {
            _mobManager = GetComponent<MobManager>();
            _animationMob = GetComponentInChildren<AnimationMob>();
        }
        private void Update()
        {
            _stateType = _animationMob.GetStateType;
            if (_stateType == StateType.Death)
            {
                DestroyMob();
            }
        }

        private void DestroyMob()
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                transform.position -= Vector3.up*Time.deltaTime;
                
                if(_timer <= -0.5f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
