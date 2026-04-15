using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Tut
{
    public class CommandParserTut 
    {
        private Regex followRegex = new Regex(@"^follow\s+(?<target>player|enemy)$", RegexOptions.IgnoreCase);


        public Node Parse(string input)
        {
            var mFollow = followRegex.Match(input);
            if (mFollow.Success)
            {
                string targetName = mFollow.Groups["target"].Value.ToLower();
                return new CommandNode("follow", new TargetNode(targetName));
            }
            return null;
        }
    }
}