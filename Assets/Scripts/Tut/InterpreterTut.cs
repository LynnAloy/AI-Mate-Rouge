using experimental;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tut
{
    public class InterpreterTut
    {
        public void Execute(Node node, GameObject obj)
        {

            if (node is CommandNode cmd && cmd.Target is TargetNode target)
            {
                if (cmd.Command == "follow")
                {
                    // 将意图传给 AIMovement 单例，由 AIMovement 负责检测与切换
                    AIMovementTut.Instance.RequestFollow(target.Name);

                    // 另外尝试立即跟随（AIMovement 内部也会尝试）
                    // 这里保留对 obj 的调用以兼容你原来的设计（如果 obj 不是 AIMovement 的实例可忽略）
                    var ai = obj.GetComponent<AIMovementTut>();
                    if (ai != null)
                    {
                        // 如果 obj 本身就是 AIMovement 的持有者，可以调用 RequestFollow
                        // 已在上面调用 AIMovement.Instance.RequestFollow(target.Name)
                    }
                }
            }
        }
    }
}