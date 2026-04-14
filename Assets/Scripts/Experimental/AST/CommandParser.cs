using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace experimental
{
    public class CommandParser
    {
        private Dictionary<string, int> keyWords;
        private TMP_Text warningText;
        private Action onFreshKeyWordsDisplay;

        private Regex followRegex = new Regex(@"^follow\s+(?<target>player|enemy)$", RegexOptions.IgnoreCase);
        private Regex attackRegex = new Regex(@"^attack\s+(?<target>player|enemy)$", RegexOptions.IgnoreCase);
        private Regex giveRegex = new Regex(@"^give\s+(?<target>weapon)$", RegexOptions.IgnoreCase);
        private Regex pickupRegex = new Regex(@"^pickup\s+(?<target>experience|keyword|money)$", RegexOptions.IgnoreCase);
        private Regex assignIntRegex = new Regex(@"^(?<lhs>[a-zA-Z_]\w*)\s*=\s*(?<value>\d+)\s*$", RegexOptions.IgnoreCase);
        private Regex assignBoolRegx = new Regex(@"^(?<lhs>[a-zA-Z_]\w*)\s*=\s*(?<value>true|false)\s*$", RegexOptions.IgnoreCase);


        private CancellationTokenSource cts;

        public CommandParser(Dictionary<string, int> keyWords, TMP_Text warningText, Action onFreshKeyWordsDisplay)
        {
            this.keyWords = keyWords;
            this.warningText = warningText;
            this.onFreshKeyWordsDisplay = onFreshKeyWordsDisplay;
        }

        public Node Parse(string input)
        {
            var mFollow = followRegex.Match(input);
            var mIntAssign = assignIntRegex.Match(input);
            var mBoolAssign = assignBoolRegx.Match(input);
            var mGive = giveRegex.Match(input);
            var mAttack = attackRegex.Match(input);
            var mPickUp = pickupRegex.Match(input);
            if (mFollow.Success)
            {
                if (keyWords != null && keyWords.TryGetValue("follow", out int count) && count > 0)
                {
                    keyWords["follow"]--;
                    onFreshKeyWordsDisplay?.Invoke();
                    string targetName = mFollow.Groups["target"].Value.ToLower();
                    return new CommandNode("follow", new TargetNode(targetName));
                }
                else
                {
                    ShowWarning(warningText, "未持有'follow'嵌入词");
                }
            }
            else if (mIntAssign.Success)
            {
                string lhs = mIntAssign.Groups["lhs"].Value;
                CheckAndConsumeKeyWord(lhs);
                int value = int.Parse(mIntAssign.Groups["value"].Value);
                return new AssignmentIntNode(lhs, value);
            }
            else if(mBoolAssign.Success)
            {
                string lhs = mBoolAssign.Groups["lhs"].Value;
                CheckAndConsumeKeyWord(lhs);
                bool value = bool.Parse(mBoolAssign.Groups["value"].Value);
                return new AssignmentBoolNode(lhs, value);
            }
            else if(mGive.Success)
            {
                if (keyWords != null && keyWords.TryGetValue("give", out int count) && count > 0)
                {
                    keyWords["give"]--;
                    onFreshKeyWordsDisplay?.Invoke();
                    string targetName = mGive.Groups["target"].Value.ToLower();
                    return new CommandNode("give", new TargetNode(targetName));
                }
                else
                {
                    ShowWarning(warningText, "未持有'give'嵌入词");
                }
            }
            else if(mAttack.Success)
            {
                if (keyWords != null && keyWords.TryGetValue("attack", out int count) && count > 0)
                {
                    keyWords["attack"]--;
                    onFreshKeyWordsDisplay?.Invoke();
                    string targetName = mAttack.Groups["target"].Value.ToLower();
                    return new CommandNode("attack", new TargetNode(targetName));
                }
                else
                {
                    ShowWarning(warningText, "未持有'attack'嵌入词");
                }
            }
            else if(mPickUp.Success)
            {
                if (keyWords != null && keyWords.TryGetValue("pickup", out int count) && count > 0)
                {
                    keyWords["pickup"]--;
                    onFreshKeyWordsDisplay?.Invoke();
                    string targetName = mPickUp.Groups["target"].Value.ToLower();
                    return new CommandNode("pickup", new TargetNode(targetName));
                }
                else
                {
                    ShowWarning(warningText, "未持有'pickup'嵌入词");
                }
            }
            else
            {
                ShowWarning(warningText, "中央处理器超载...无法解析指令");
            }
            return null;
        }

        private void CheckAndConsumeKeyWord(string keyWord)
        {
            if (keyWords != null && keyWords.TryGetValue(keyWord, out int count) && count > 0)
            {
                keyWords[keyWord]--;
                onFreshKeyWordsDisplay?.Invoke();
            }
            else
            {
                ShowWarning(warningText, $"未持有'{keyWord}'嵌入词");
            }
        }

        public async void ShowWarning(TMP_Text warningText, string message)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            await ShowWarningAsync(warningText, message, cts.Token);
        }

        private async Task ShowWarningAsync(TMP_Text warningText, string message, CancellationToken ct)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = message;
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2), ct);
                warningText.gameObject.SetActive(false);
            }
            catch (TaskCanceledException)
            {
                // Ignore cancellation
            }
        }

        public void CancelWarning()
        {
            cts?.Cancel();
        }

    }
}