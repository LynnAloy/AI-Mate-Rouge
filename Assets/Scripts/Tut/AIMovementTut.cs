using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tut
{
    public class AIMovementTut : Singleton<AIMovementTut>
    {
        [SerializeField] private float aiSpeed = 4.0f;
        [SerializeField] private float desiredDistance = 0.5f;
        private Coroutine followCoroutine;

        // 意图标志
        private bool canFollowPlayer = false;
        private bool canFollowEnemy = false;

        // 当前正在跟随的目标（用于判断和切换）
        private GameObject currentTarget;



        // 外部调用：设置意图并尝试立即跟随（若目标存在）
        public void RequestFollow(string targetName)
        {
            Debug.Log($"AI Movement: Before: canFollowPlayer = {canFollowPlayer}, canFollowEnemy = {canFollowEnemy}");
            if (targetName == "player")
            {
                canFollowPlayer = true;
                TryFollowBestTarget();
            }
            else if (targetName == "enemy")
            {
                canFollowEnemy = true;
                TryFollowBestTarget();
            }
            Debug.Log($"AI Movement: After: canFollowPlayer = {canFollowPlayer}, canFollowEnemy = {canFollowEnemy}");
        }

        // 外部调用：取消某个意图（可选）
        public void CancelFollow(string targetName)
        {
            if (targetName == "player") canFollowPlayer = false;
            if (targetName == "enemy") canFollowEnemy = false;
            // 如果当前目标对应被取消，则停止
            if (currentTarget != null && currentTarget.CompareTag("Player") && !canFollowPlayer) StopFollowing();
            if (currentTarget != null && currentTarget.CompareTag("Enemy") && !canFollowEnemy) StopFollowing();
        }

        // 尝试根据优先级选择并跟随目标
        private void TryFollowBestTarget()
        {
            // 优先跟随存在的 enemy（如果允许）
            if (canFollowEnemy)
            {
                var enemy = GameObject.FindWithTag("Enemy");
                if (enemy != null)
                {
                    StartFollowing(enemy);
                    return;
                }
            }

            // 否则尝试跟随 player（如果允许）
            if (canFollowPlayer)
            {
                var player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    StartFollowing(player);
                    return;
                }
            }

            // 如果没有可用目标，则启动监视协程，在目标出现时自动跟随
            StartCoroutine(WatchForTargetsCoroutine());
        }

        // 开始跟随指定目标
        private void StartFollowing(GameObject target)
        {
            if (target == null) return;
            if (currentTarget == target) return; // 已经在跟随
            StopFollowing();
            currentTarget = target;
            Debug.Log($"AI Movement: Start following {target.name}");
            followCoroutine = StartCoroutine(FollowTargetCoroutine(target));
        }

        // 停止当前跟随
        private void StopFollowing()
        {
            if (followCoroutine != null)
            {
                StopCoroutine(followCoroutine);
                followCoroutine = null;
            }
            currentTarget = null;
        }

        // 主跟随协程
        private IEnumerator FollowTargetCoroutine(GameObject target)
        {
            float stopSqrt = desiredDistance * desiredDistance;
            while (target != null)
            {
                float distanceSqrt = (transform.position - target.transform.position).sqrMagnitude;
                if (distanceSqrt <= stopSqrt)
                {
                    // 已经在期望距离内，停止移动但继续监视目标状态
                    yield return null;
                }
                else
                {
                    // 计算目标点：在目标周围 desiredDistance 处的点（从目标指向 AI 的方向）
                    Vector3 dirFromTarget = (transform.position - target.transform.position).normalized;
                    Vector3 desiredPos = target.transform.position + dirFromTarget * desiredDistance;

                    // 如果 AI 恰好在目标点的另一侧（dirFromTarget 可能为零），退回到直接 MoveTowards 目标位置
                    if (dirFromTarget == Vector3.zero)
                    {
                        desiredPos = target.transform.position + Vector3.forward * desiredDistance;
                    }
                    Vector3 prePos = transform.position;
                    // 向 desiredPos 移动（而不是直接到 target）
                    transform.position = Vector3.MoveTowards(transform.position, desiredPos, aiSpeed * Time.deltaTime);
                    // 更新动画参数
                    Vector3 moveDelta = transform.position - prePos;
                    
                    float frameDistance = moveDelta.magnitude;
                    float currentSpeed = frameDistance / Mathf.Max(Time.deltaTime, 1e-6f);
                    
                }
                if (currentTarget != null && currentTarget.CompareTag("Player") && canFollowEnemy && GameObject.FindWithTag("Enemy") != null)
                {
                    GameObject enemy = GameObject.FindWithTag("Enemy");
                    if (enemy != null)
                    {
                        StartFollowing(enemy);
                        yield break;
                    }
                }
                yield return null;
            }

            // 目标消失后，清空 currentTarget 并尝试根据意图切换到其他目标
            currentTarget = null;
            // 目标消失后继续监视，若意图仍在则会在 WatchForTargetsCoroutine 中接手
            StartCoroutine(WatchForTargetsCoroutine());
        }

        // 监视场景中目标何时出现（轻量轮询）
        private IEnumerator WatchForTargetsCoroutine()
        {
            // 如果已有正在监视的协程则不重复启动
            // 这里用 local coroutine variable 简化：如果 followCoroutine != null 表示正在跟随，不需要监视
            while (currentTarget == null && (canFollowEnemy || canFollowPlayer))
            {
                // 优先检查 enemy
                if (canFollowEnemy)
                {
                    var enemy = GameObject.FindWithTag("Enemy");
                    if (enemy != null)
                    {
                        StartFollowing(enemy);
                        yield break;
                    }
                }

                // 再检查 player
                if (canFollowPlayer)
                {
                    var player = GameObject.FindWithTag("Player");
                    if (player != null)
                    {
                        StartFollowing(player);
                        yield break;
                    }
                }

                // 每帧检查一次（你也可以改为每 0.2s 检查一次以降低开销）
                yield return null;
            }
        }
    }
}