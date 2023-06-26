using UnityEngine;

namespace TowerDefense
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private TypeTower _typeTower;
        [SerializeField] private TowerDamageEffects _towerDamageEffects;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private bool _aoeOn;
        [SerializeField] private float _aoeAreaDamage;
        [SerializeField] private float _aoeAreaEffect;
        [SerializeField] private float _timeToSlowMob;
        [SerializeField] private float _timeToPoisonMob;
        [SerializeField] private float _poisonDamage;
        [SerializeField] private float _moveSpeedProjectile;
        [SerializeField] private GameObject _target;
        [SerializeField] Vector3 _targetPosition;
        [SerializeField] private int _damage;
        [SerializeField] private Collider[] _mobInAOEForDamage;
        [SerializeField] private Collider[] _mobInAOEForEffect;
        private TowerManager _towerManager;
        private GameManager _gameManager;
        const int mobLayerMask = 1 << 6;
        [SerializeField] private float _timerLifeProj;
        [SerializeField] private bool _destroyProjectile = false;

        public float GetTimeToPoisonDamage => _timeToPoisonMob;
        public float GetPoisonDamage => _poisonDamage;
        private void Awake()
        {
            _target = GetComponentInParent<TowerAction>().GetTarget;
            _towerManager = GetComponentInParent<TowerManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _audioSource = GetComponent<AudioSource>();
            _aoeOn = _towerManager.GetAOEDamage;
            _aoeAreaDamage = _towerManager.GetAOEAreaDamage;
            _aoeAreaEffect = _towerManager.GetAOEAreaEffect;
            _damage = _towerManager.GetAttackDamage;
            _timeToSlowMob = _gameManager.GetTimeToSlowMob;
            _timeToPoisonMob = _gameManager.GetTimeToPoisonMob;
            _poisonDamage = _gameManager.GetPoisonDamage;
            _timerLifeProj = 1;
        }
        private void Start()
        {
            _audioSource.Play();
        }
        private void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if (_aoeOn == true)
                {
                    FindMobForAOEDamage(_target);
                }
                else
                {
                    if (_towerDamageEffects == TowerDamageEffects.Freezing)
                    {
                        FindMobForAOEEffect(_target);
                    }
                }

                _targetPosition = _target.transform.position;
                _moveSpeedProjectile = _towerManager.GetMooveSpeedProjectile * Time.deltaTime;
                ProjectileMoving(_targetPosition);
            }

            if (_destroyProjectile == true)
            {
                DestroyProjectile();
            }
        }
        public void ProjectileMoving(Vector3 targetPosition)
        {
            targetPosition.y += 0.7f;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeedProjectile);
            transform.LookAt(targetPosition);
        }
        private void OnTriggerEnter(Collider collider)
        {
            if(collider == _target.GetComponent<Collider>() && collider.gameObject.tag == "Mob") // первую проверку можно будет убрать если  снаряды будут лететь по траектории сверху вниз
            {
                if(_aoeOn == false)
                {
                    if(_towerDamageEffects == TowerDamageEffects.None)
                    {
                        Debug.Log("Попал в одиночную цель");
                        DealingDamageForMob();
                        DisableProjectil(gameObject);
                    }
                    else if (_towerDamageEffects == TowerDamageEffects.Freezing)
                    {
                        Debug.Log("Наложил эффект Холода");
                        DealingDamageForMob();
                        foreach (var mob in _mobInAOEForEffect)
                        {
                            mob.GetComponent<MobManager>().SetMobSlowMove = true;
                            mob.GetComponent<MobManager>().SetTimeForSlowMob = _timeToSlowMob;
                        }
                        DisableProjectil(gameObject);
                    }
                    else if (_towerDamageEffects == TowerDamageEffects.Poisoning)
                    {
                        var target = _target.GetComponent<MobManager>();
                        Debug.Log("Цель Отравлена");
                        DealingDamageForMob();
                        target.SetPoisonEffectOnMob = true;
                        target.SetTimeToPoison = _timeToPoisonMob;
                        target.SetPoisonDamage = _poisonDamage;
                        DisableProjectil(gameObject);
                    }
                }
                else
                {
                    Debug.Log("Попал по нескольким");
                    foreach(var mob in _mobInAOEForDamage)
                    {
                        mob.GetComponent<MobManager>().HealthMob -= _damage;
                    }
                    DisableProjectil(gameObject);
                }
            }
        }
        private void FindMobForAOEDamage(GameObject target)
        {
            Vector3 mobPosition = target.transform.position;
            _mobInAOEForDamage = Physics.OverlapSphere(mobPosition, _aoeAreaDamage, mobLayerMask);
        }
        private void FindMobForAOEEffect(GameObject target)
        {
            Vector3 mobPosition = target.transform.position;
            _mobInAOEForEffect = Physics.OverlapSphere(mobPosition, _aoeAreaEffect, mobLayerMask);
        }

        #region Расчёт наносимого урона мобу с учётом его брони
        private void DealingDamageForMob()
        {
            var target = _target.GetComponent<MobManager>();
            ArmorMobType armorType = target.GetComponent<MobManager>().GetArmorMobType;
            
            if(_typeTower == TypeTower.ArrowTower)
            {
                if(armorType == ArmorMobType.Light)
                {
                    int cofDamage = (int)DamageTypeArmorType.ArrowLight;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Medium)
                {
                    int cofDamage = (int)DamageTypeArmorType.ArrowMedium;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Heavy)
                {
                    int cofDamage = (int)DamageTypeArmorType.ArrowHeavy;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
            }
            else if(_typeTower == TypeTower.SiegeTower)
            {
                if (armorType == ArmorMobType.Light)
                {
                    int cofDamage = (int)DamageTypeArmorType.SiegeLight;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Medium)
                {
                    int cofDamage = (int)DamageTypeArmorType.SiegeMedium;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Heavy)
                {
                    int cofDamage = (int)DamageTypeArmorType.SiegeHeavy;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
            }
            else if(_typeTower == TypeTower.IceTower)
            {
                if (armorType == ArmorMobType.Light)
                {
                    int cofDamage = (int)DamageTypeArmorType.IceLight;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Medium)
                {
                    int cofDamage = (int)DamageTypeArmorType.IceMedium;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Heavy)
                {
                    int cofDamage = (int)DamageTypeArmorType.IceHeavy;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
            }
            else if(_typeTower == TypeTower.PoisonTower)
            {
                if (armorType == ArmorMobType.Light)
                {
                    int cofDamage = (int)DamageTypeArmorType.PoisonLight;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Medium)
                {
                    int cofDamage = (int)DamageTypeArmorType.PoisonMedium;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Heavy)
                {
                    int cofDamage = (int)DamageTypeArmorType.PoisonHeavy;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
            }
            else if (_typeTower == TypeTower.ChaosTower)
            {
                if (armorType == ArmorMobType.Light)
                {
                    int cofDamage = (int)DamageTypeArmorType.ChaosLight;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Medium)
                {
                    int cofDamage = (int)DamageTypeArmorType.ChaosMedium;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
                else if (armorType == ArmorMobType.Heavy)
                {
                    int cofDamage = (int)DamageTypeArmorType.ChaosnHeavy;
                    target.HealthMob -= _damage * (cofDamage / 100f);
                }
            }
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (_aoeOn == true)
            {
                Gizmos.color = Color.green;
                Vector3 position = transform.position;
                Gizmos.DrawWireSphere(position, _aoeAreaDamage);
            }
            else
            {
                if(_towerDamageEffects == TowerDamageEffects.Freezing)
                {
                    Gizmos.color = Color.blue;
                    Vector3 position = transform.position;
                    Gizmos.DrawWireSphere(position, _aoeAreaEffect);
                }
            }
        }
        private void DisableProjectil(GameObject towerProjectile)
        {
            towerProjectile.GetComponent<Collider>().enabled = false;
            towerProjectile.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            towerProjectile.transform.GetChild(1).gameObject.SetActive(true);
            _destroyProjectile = true;
        }
        private void DestroyProjectile()
        {
            _timerLifeProj -= Time.deltaTime;
            if(_timerLifeProj <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

