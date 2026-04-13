using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float pickupRange;
    [SerializeField] private List<Weapon> unassignedWeapons;
    [SerializeField] private List<Weapon> assignedWeapons;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private List<Weapon> runtimeUnassignedWeapons;

    protected override void Awake()
    {
        base.Awake();
        runtimeUnassignedWeapons = new List<Weapon>(unassignedWeapons);
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        if(animator == null)
        {
            Debug.LogError("PlayerController: Animator component not found in children.");
        }
        if(spriteRenderer == null)
        {
            Debug.Log("PlayerController: SpriteRenderer component not found in children.");
        }
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(GetRandomWeaponIndex());
        }
        foreach(var weapon in assignedWeapons)
        {
            weapon.gameObject.SetActive(true);
        }
        foreach(var weapon in assignedWeapons)
        {
            Debug.Log($"PlayerController: Assigned weapon is {weapon == null}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new(horizontal, vertical);
        moveInput.Normalize();
        if(moveInput != Vector2.zero)
        {
            if(horizontal < 0)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("isRunning", true);
            }
            else if(horizontal > 0)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //按下Spacebar 闪避
    }

    private void FixedUpdate()
    {
        // 物理移动在 FixedUpdate 中执行
        Vector2 desired = rb.position + playerMoveSpeed * Time.fixedDeltaTime * moveInput;
        // Clamp 到矩形边界
        desired.x = Mathf.Clamp(desired.x, minBounds.x, maxBounds.x);
        desired.y = Mathf.Clamp(desired.y, minBounds.y, maxBounds.y);
        rb.MovePosition(desired);
    }

    public int GetRandomWeaponIndex()
    {
        return UnityEngine.Random.Range(0, runtimeUnassignedWeapons.Count);
    }

    public void AddWeapon(int weaponIndex)
    {
        if (weaponIndex < runtimeUnassignedWeapons.Count)
        {
            var weapon = runtimeUnassignedWeapons[weaponIndex];
            assignedWeapons.Add(weapon);
            weapon.gameObject.SetActive(true);
            runtimeUnassignedWeapons.RemoveAt(weaponIndex);
        }
    }

    public float GetPickUpRange()
    {
        return pickupRange;
    }

    public void SetPickUpRange(float ratio)
    {
        pickupRange *= ratio;
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public void SetPlayerMoveSpeed(float ratio)
    {
        playerMoveSpeed *= ratio;
        playerMoveSpeed = Mathf.Clamp(playerMoveSpeed, 0.1f, 20f);
    }

    public List<Weapon> GetAssignedWeapons()
    {
        return assignedWeapons;
    }

    public List<Weapon> GetUnassignedWeapons()
    {
        return runtimeUnassignedWeapons;
    }
}
