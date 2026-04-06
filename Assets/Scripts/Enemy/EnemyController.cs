using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB;
    [SerializeField] private Transform target;
    [SerializeField] private float slowRadius;
    [SerializeField] private Animator animator;
    [SerializeField] private float hitWaitTime;
    [SerializeField] private float knockBackTime;

    [SerializeField] private EnemySO enemySO;
    private float minMoveSpeed;
    private float maxMoveSpeed;
    private float damage;
    private float currentHealth;
    private SpriteRenderer spriteRenderer;
    private int enemyLevel;
    private int expToDrop;

    private float hitTimer;

    private List<EnemyController> enemies = new();

    private void Awake()
    {
        minMoveSpeed = enemySO.EnemyMinMoveSpeed;
        maxMoveSpeed = enemySO.EnemyMaxMoveSpeed;
        damage = enemySO.EnemyDamage;
        currentHealth = enemySO.EnemyHealth;
        enemyLevel = enemySO.EnemyLevel;
        expToDrop = enemySO.ExpToDrop;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = enemySO.EnemySprite;
        animator.runtimeAnimatorController.animationClips[0] = enemySO.Idle;
        //Debug.Log(currentHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();

        if(hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    private void EnemyMove()
    {
        if(target == null)
        {
            return;
        }
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        if (distance >= slowRadius)
        {
            RB.velocity = direction.normalized * maxMoveSpeed;
            float vx = RB.velocity.x;
            if (vx > 0)
            {
                animator.SetBool("isMoving", true);
            }
            else if (vx < 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            RB.velocity = direction.normalized * Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distance / slowRadius);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        //Debug.Log("Damaged taken.");
        currentHealth -= damageToTake;
        if(currentHealth <= 0)
        {
            ExperienceLevelController.Instance.SpawnExp(transform.position, expToDrop);
            Destroy(gameObject);
        }

        DamageNumberController.Instance.SpawnDamageNumber(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool canKnockBack, float knockBackDistance)
    {
        TakeDamage(damageToTake);
        if (canKnockBack)
        {
            Vector3 knockBackDirection = (transform.position - target.position).normalized;
            StartCoroutine(KnockBackCoroutine(knockBackDirection, knockBackDistance, knockBackTime));
        }
    }

    private IEnumerator KnockBackCoroutine(Vector3 direection, float distance, float duration)
    {
        
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + direection * distance;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && hitTimer <= 0f)
        {
            PlayerHealthController.Instance.TakeDamage(damage);
            hitTimer = hitWaitTime;
        }
    }

    public int GetEnemyLevel()
    {
        return enemyLevel;
    }

}

