using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace experimental
{
    public class CommandParser
    {
        private Regex followRegex = new Regex(@"^follow\s+player$");

        public CommandNode Parse(string input)
        {
            if (followRegex.IsMatch(input))
            {
                return new CommandNode("follow", new TargetNode("player"));
            }
            Debug.Log("Invalid command.");
            return null;
        }
    }
}