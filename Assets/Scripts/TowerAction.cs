using System.Collections;
using UnityEngine;

namespace TowerDefense
{
    public class TowerAction : MonoBehaviour
    {
        [SerializeField] private Collider[] _targetsCollider;
        [SerializeField] private GameObject _towerTarget;
        [SerializeField] private GameObject _towerProjectile;
        private TowerManager _towerManager;
        [SerializeField] private float _timer;
        const int mobLayerMask = 1 << 6;
        private int _startTImeForProjectile = 10;

        public GameObject GetTarget
        {
            get => _towerTarget;
        }
        private void Awake()
        {
            _towerManager = GetComponent<TowerManager>();
            //_timer = _startTImeForProjectile;
        }
        private void Update()
        {
            Physics.SyncTransforms();

            _timer += Time.deltaTime;
            if (TrackTarget() || FindTarget())
            {
                if (_timer >= (60f/_towerManager.GetAttackSpeed))
                {
                    StartCoroutine(CreateTowerProjectile(_towerProjectile));
                    _timer = 0;
                }
            }
        }
        public bool FindTarget()
        {
            Vector3 towerPos = transform.position;
            Vector3 hightCapsule = towerPos;
            hightCapsule.y += 5f;
            _targetsCollider = Physics.OverlapCapsule(towerPos, hightCapsule, _towerManager.GetAttackRange, mobLayerMask);
            if(_targetsCollider.Length > 0)
            {
                _towerTarget = _targetsCollider[0].gameObject;
                if(_towerTarget.GetComponentInChildren<AnimationMob>().GetStateType == StateType.Death)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                _towerTarget = null;
                return false;
            }
        }
        public bool TrackTarget()
        {
            if(_towerTarget == null || _towerTarget.GetComponentInChildren<AnimationMob>().GetStateType == StateType.Death)
            {
                return false;
            }
            else
            {
                Vector3 towerPosition = transform.position;
                Vector3 targetPosition = _towerTarget.transform.position;
                if (Vector3.Distance(towerPosition, targetPosition) > _towerManager.GetAttackRange + 0.05f) // + _towerTarget.GetComponent<CapsuleCollider>().radius*_towerTarget.transform.localScale.y)
                {
                    _towerTarget = null;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private IEnumerator CreateTowerProjectile(GameObject towerProjectile)
        {
            Vector3 startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
            Quaternion startRotation = new Quaternion(0, 0, 0, 0);
            Instantiate(towerProjectile, startPosition, startRotation, gameObject.transform);
            yield return null;
        }
        private void OnDrawGizmos()
        {
            if (_towerTarget != null)
            {
                Gizmos.DrawLine(transform.position, _towerTarget.transform.position);
            }
        }
    }
}
