using System.Collections;
using UnityEngine;

namespace experimental
{
    public class AIMovement : Singleton<AIMovement>
    {
        [SerializeField] private float aiSpeed = 4.0f;
        private Coroutine followCoroutine;

        // 意图标志
        private bool canFollowPlayer = false;
        private bool canFollowEnemy = false;

        // 当前正在跟随的目标（用于判断和切换）
        private GameObject currentTarget;

        // 外部调用：设置意图并尝试立即跟随（若目标存在）
        public void RequestFollow(string targetName)
        {
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
            while (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, aiSpeed * Time.deltaTime);
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
