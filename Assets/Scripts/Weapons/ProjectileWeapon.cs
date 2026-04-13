using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    //References
    [SerializeField] private EnemyDamager damager;
    [SerializeField] private Projectile projectile;
    //Variables
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform shootPoint;

    private float shootCounter;
    private float baseAttackRange;
    private float baseTimeBetweenAttack;

    protected override void Awake()
    {
        base.Awake();
        baseAttackRange = attackRange;
        baseTimeBetweenAttack = timeBetweenAttack;
    }

    // Start is called before the first frame update
    void Start()
    {
        HasLeveledUp += SetProjectileWeapon;
        shootCounter = timeBetweenAttack;
    }

    // Update is called once per frame
    void Update()
    {
        shootCounter -= Time.deltaTime;
        if( shootCounter <= 0 )
        {
            shootCounter = timeBetweenAttack;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);
            if (enemies.Length > 0)
            {
                Vector3 targetPosition = enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position;

                Vector3 direction = targetPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Projectile newprojectile = Instantiate(projectile, shootPoint.position, projectile.transform.rotation);
                newprojectile.gameObject.SetActive(true);
                Debug.Log($"Is projectile null: {newprojectile == null}; spawnPos : {shootPoint.position}; Is projectile active : {newprojectile.isActiveAndEnabled}");
            }
        }
    }


    private void SetProjectileWeapon()
    {
        attackRange = baseAttackRange * LevelToRatioAttackRange(WeaponLevel);
        timeBetweenAttack = baseTimeBetweenAttack * LevelToTimeBetweenAttack(WeaponLevel);
    }

    private void OnDestroy()
    {
        HasLeveledUp -= SetProjectileWeapon;
    }
}
