using UnityEngine;

namespace experimental
{
    public class Interpreter
    {
        public void Execute(Node node, GameObject obj)
        {

            if (node is CommandNode cmd && cmd.Target is TargetNode target)
            {
                if (cmd.Command == "follow")
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
                else if(cmd.Command == "give")
                {
                    if(target.Name == "weapon")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.GiveWeapon();
                    }
                }
                else if(cmd.Command == "attack")
                {
                    if(target.Name == "player")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.SetCanAIAttackPlayer(true);
                    }
                    else if(target.Name == "enemy")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.SetCanAIAttackEnemy(true);
                    }
                }
                else if(cmd.Command == "pickup")
                {
                    if(target.Name == "experience")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.SetCanPickUpExp(true);
                    }
                    else if(target.Name == "keyword")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.SetCanPickUpKeyWord(true);
                    }
                    else if(target.Name == "money")
                    {
                        AIController aiController = TryGetAIController(obj);
                        if (aiController == null) return;
                        AIController.Instance.SetCanPickUpCoin(true);
                    }
                }
            }
            else if(node is AssignmentIntNode aInt)
            {
                AIController aiController = TryGetAIController(obj);
                if (aiController == null) return;
                if (aInt.Lhs == "level")
                {
                    AIController.Instance.SetCurrentLevel(((AssignmentIntNode)node).Value);
                }
            }
            else if(node is AssignmentBoolNode aBool)
            {
                AIController aiController = TryGetAIController(obj);
                if (aiController == null) return;
                if (aBool.Lhs == "canHeal")
                {
                    AIController.Instance.SetHeal(((AssignmentBoolNode)node).Value);
                }

            }
        }
        private AIController TryGetAIController(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogError("Interpreter: Assignment node requires a valid GameObject.");
                return null;
            }
            AIController aiController = obj.GetComponent<AIController>();
            if (aiController == null)
            {
                Debug.LogError("Interpreter: GameObject does not have an AIController component.");
                return null;
            }
            return aiController;
        }
    }
}
