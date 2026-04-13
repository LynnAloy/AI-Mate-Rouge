using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour, IDamager
{
    //References
    [SerializeField] private DamagerSO damagerSO;
    [SerializeField] private DamagerSO baseDamagerSO;
    //Variables
    [SerializeField] private float growSpeed;
    [SerializeField] private float a;//a stands for log function base

    //Variables from damagerSO
    private float damageAmount;
    private float damagerSize;
    private bool isExternal;
    private bool destroyParent;
    private bool canChangeSize;
    private bool canKnockBack;
    private float knockBackDistance;
    private float lifeTime;
    private float changeSizeTime;
    private bool isAreaDamage;
    private float areaDamageInterval;
    private bool destroyOnImpact;

    private float baseDamageAmount;
    private float baseDamagerSize;

    //local variable
    private Transform damagerParent;
    private Vector3 targetSize;
    private float areaDamagerTimer; //how often trigger area attack weapon damage
    private HashSet<EnemyController> enemiesInRange = new();
    private bool isInitialized = false;
    //private static bool hasInitializedBase = false;//由于在运行时写入了SO，所以如果重新开始游戏需要重新初始化

    private void OnEnable()
    {
        /*
        if (!hasInitializedBase)
        {
            InitEnemyDamagerBase();
            hasInitializedBase = true;
        }
        */
        if (!isInitialized)
        {
            InitEnemyDamager();
            isInitialized = true;
        }
        
    }


    private void Awake()
    {
        if(damagerSO == null)
        {
            Debug.LogError("EnemyDamager: DamagerSO reference not set.");
        }
        /*
        if (hasInitializedBase)
        {
            InitEnemyDamagerBase();
            hasInitializedBase = true;
        }
        */
        if (!isInitialized)
        {
            InitEnemyDamager();
        }
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
        areaDamagerTimer = areaDamageInterval;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (areaDamagerTimer > 0)
        {
            areaDamagerTimer -= Time.deltaTime;
        }
        if (canChangeSize)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
            changeSizeTime -= Time.deltaTime;
            if (changeSizeTime <= 0f)
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
        else if(!canChangeSize && !isAreaDamage && !destroyOnImpact)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0f)
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
        if (isAreaDamage && areaDamagerTimer <= 0)
        {
            if (enemiesInRange.Count > 0)
            {
                var enemies = new EnemyController[enemiesInRange.Count];
                enemiesInRange.CopyTo(enemies);
                foreach (var e in enemies)
                {
                    if (e != null)
                    {
                        e.TakeDamage(damageAmount);
                    }
                    else
                    {
                        enemiesInRange.Remove(e);
                    }
                }
            }
            areaDamagerTimer = areaDamageInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAreaDamage == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("Hit");
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, canKnockBack, knockBackDistance);
                if (destroyOnImpact == true)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                if(collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    private void InitEnemyDamager()
    {
        damageAmount = damagerSO.DamageAmount;
        damagerSize = damagerSO.DamagerSize;
        gameObject.transform.localScale = Vector3.one * damagerSize;
        targetSize = new Vector3(damagerSize, damagerSize, 1);
        isExternal = damagerSO.IsExternal;
        destroyParent = damagerSO.DestroyParent;
        canChangeSize = damagerSO.CanChangedSize;
        canKnockBack = damagerSO.CanKnockBack;
        knockBackDistance = damagerSO.KnockBackDistance;
        lifeTime = damagerSO.LifeTime;
        changeSizeTime = damagerSO.ChangeSizeTime;
        isAreaDamage = damagerSO.IsAreaDamage;
        areaDamageInterval = damagerSO.AreaDamageInterval;
        destroyOnImpact = damagerSO.DestroyOnImpact;
        baseDamageAmount = damageAmount;
        baseDamagerSize = damagerSize;
    }

    private void LevelUpToDamage(int currentLevel)
    {
        damageAmount = baseDamageAmount * (Mathf.Log(currentLevel, a) + 1);
        damagerSO.DamageAmount = damageAmount;
    }
   
    private void LevelUpToDamagerSize(int currentLevel)
    {
        damagerSize = baseDamagerSize * (Mathf.Log(currentLevel, a) + 1);
        damagerSO.DamagerSize = damagerSize;
    }

    public void OnWeaponLevelUp(int weaponLevel)
    {
        LevelUpToDamage(weaponLevel);
        LevelUpToDamagerSize(weaponLevel);
    }

    public void SetDamageAmount(float damage)
    {
        damageAmount = damage;
        damagerSO.DamageAmount = damageAmount;
    }

    public float GetDamageAmount()
    {
        /*
        if(!hasInitializedBase)
        {
            InitEnemyDamagerBase();
            hasInitializedBase = true;
        }
        */
        if(!isInitialized)
        {
            InitEnemyDamager();
        }
        return damageAmount;
    }

    private void InitEnemyDamagerBase()
    {
        damagerSO.DamageAmount = baseDamagerSO.DamageAmount;
        damagerSO.DamagerSize = baseDamagerSO.DamagerSize;
        gameObject.transform.localScale = Vector3.one * damagerSO.DamagerSize;
        targetSize = new Vector3(damagerSO.DamagerSize, damagerSO.DamagerSize, 1);
    }
}
