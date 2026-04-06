using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace experimental
{
    public class Interpreter
    {
        public void Execute(CommandNode node, GameObject obj)
        {
            if (node.Command == "follow" && node.Target is TargetNode target && target.Name == "player")
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player == null)
                {
                    Debug.LogError("Player not found in the scene.");
                }
                else
                {
                    Debug.Log("Player is not null.");
                }
                obj.GetComponent<AIMovement>().FollowPlayer(player);
            }
        }
    }
}