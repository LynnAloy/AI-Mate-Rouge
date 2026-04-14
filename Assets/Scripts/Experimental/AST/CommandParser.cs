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
        private Regex pickupRegex = new Regex(@"^pickup\s+(?<target>experience|keyword)$", RegexOptions.IgnoreCase);
        private Regex assignRegex = new Regex(@"^(?<lhs>[a-zA-Z_]\w*)\s*=\s*(?<op1>[a-zA-Z_]\w*|\d+)\s*(?<op>[+\-*/])?\s*(?<op2>[a-zA-Z_]\w*|\d+)?$", RegexOptions.IgnoreCase);

        private CancellationTokenSource cts;

        public CommandParser(Dictionary<string, int> keyWords, TMP_Text warningText, Action onFreshKeyWordsDisplay)
        {
            this.keyWords = keyWords;
            this.warningText = warningText;
            this.onFreshKeyWordsDisplay = onFreshKeyWordsDisplay;
        }

        public CommandNode Parse(string input)
        {
            var mFollow = followRegex.Match(input);
            var mAssign = assignRegex.Match(input);
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
            
            else
            {
                ShowWarning(warningText, "中央处理器超载...无法解析指令");
            }
            return null;
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