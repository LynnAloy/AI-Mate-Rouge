using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace experimental
{
    public class CommandParser
    {
        private Regex followRegex = new Regex(@"^follow\s+(?<target>player|enemy)$", RegexOptions.IgnoreCase);
        private Regex attackRegex = new Regex(@"^attack\s+(?<target>player|enemy)$");

        public CommandNode Parse(string input)
        {
            var m = followRegex.Match(input);
            if(m.Success)
            {
                string targetName = m.Groups["target"].Value.ToLower();
                return new CommandNode("follow", new TargetNode(targetName));
            }
            Debug.Log("Invalid command.");
            return null;
        }
    }
}