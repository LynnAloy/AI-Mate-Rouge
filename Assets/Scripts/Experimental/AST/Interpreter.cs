using UnityEngine;

namespace experimental
{
    public class Interpreter
    {
        public void Execute(CommandNode node, GameObject obj)
        {
            if (node.Command == "follow" && node.Target is TargetNode target)
            {
                // 将意图传给 AIMovement 单例，由 AIMovement 负责检测与切换
                AIMovement.Instance.RequestFollow(target.Name);

                // 另外尝试立即跟随（AIMovement 内部也会尝试）
                // 这里保留对 obj 的调用以兼容你原来的设计（如果 obj 不是 AIMovement 的实例可忽略）
                var ai = obj.GetComponent<AIMovement>();
                if (ai != null)
                {
                    // 如果 obj 本身就是 AIMovement 的持有者，可以调用 RequestFollow
                    // 已在上面调用 AIMovement.Instance.RequestFollow(target.Name)
                }
            }
        }
    }
}
