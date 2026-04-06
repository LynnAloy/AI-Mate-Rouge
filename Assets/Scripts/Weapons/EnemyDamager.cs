using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    //Variables
    [SerializeField] private float growSpeed;

    //Variables from weaponSO
    private float damageAmount;
    private bool isExternal;
    private bool canChangeSize;
    private bool canKnockBack;
    private float knockBackDistance;
    private bool destroyParent;
    private float duration;
    private float attackRange;
    private Transform damagerParent;
    private Vector3 targetSize;

    private Weapon weapon;

    private void Awake()
    {
        //Get values from weaponSO
        weapon = GetComponentInParent<Weapon>();
        InitEnemyDamager();
        //Debug.Log($"DamageAmount: {damageAmount}");
    }

    // Start is called before the first frame update
    void Start()
    {
        damagerParent = gameObject.transform.parent;
        if (canChangeSize)
        {
            targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeSize)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                targetSize = Vector3.zero;
                if (transform.localScale.x == 0 && !isExternal && destroyParent)
                {
                    Destroy(damagerParent.gameObject);
                }
                else if (transform.localScale.x == 0 && !isExternal)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                if (!isExternal && destroyParent)
                {
                    Destroy(damagerParent.gameObject);
                }
                else if (!isExternal)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Hit");
            collision.GetComponent<EnemyController>().TakeDamage(damageAmount, canKnockBack, knockBackDistance);
        }
    }

    private void InitEnemyDamager()
    {
        damageAmount = weapon.GetWeaponDamage();
        isExternal = weaponSO.IsExternal;
        canChangeSize = weaponSO.CanChangeSize;
        canKnockBack = weaponSO.CanKnockBack;
        knockBackDistance = weaponSO.KnockBackDistance;
        destroyParent = weaponSO.DestoryParent;
        duration = weaponSO.Duration;
        attackRange = weaponSO.AttackRange;
    }


    public void SetDamage(float ratio)
    {
        damageAmount *= ratio;
    }

    public void SetDuration(float ratio)
    {
        duration *= ratio;
    }

    public void SetTargetSize(float ratio)
    {
        targetSize = attackRange * ratio * targetSize;
    }
}
