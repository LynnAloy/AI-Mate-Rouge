using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetDifficultyInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField bossWaveInput;
    [SerializeField] private TMP_InputField maxWaveInput;
    [SerializeField] private TMP_Text errorText;

    private int bossWaveValue;
    private int maxWaveValue;

    private void Start()
    {
        // 强制输入为数字键（但 IntegerNumber 仍允许负号，所以我们再用 onValidateInput 拦截符号）
        bossWaveInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        maxWaveInput.contentType = TMP_InputField.ContentType.IntegerNumber;

        // 验证器：只允许数字字符（0-9），禁止 '-' '+' '.' 等
        bossWaveInput.onValidateInput += ValidateDigitOnly;
        maxWaveInput.onValidateInput += ValidateDigitOnly;

        // 不同字段使用不同的 EndEdit 回调以便分别保存
        bossWaveInput.onEndEdit.AddListener(OnBossWaveEndEdit);
        bossWaveInput.onValueChanged.AddListener(OnValueChanged);

        maxWaveInput.onEndEdit.AddListener(OnMaxWaveEndEdit);
        maxWaveInput.onValueChanged.AddListener(OnValueChanged);

        errorText.text = "";
    }

    // 验证函数：只允许数字字符；返回 '\0' 表示拒绝该字符（TMP 会忽略）
    private char ValidateDigitOnly(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar)) return addedChar;
        return '\0';
    }

    // 通用实时清理错误提示
    private void OnValueChanged(string s)
    {
        errorText.text = "";
    }

    // BossWave 的结束编辑处理：解析并保存
    private void OnBossWaveEndEdit(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            errorText.text = "Boss 波数不能为空";
            return;
        }

        if (int.TryParse(s, out int v))
        {
            if (v <= 0)
            {
                errorText.text = "Boss 波数必须为正整数";
                return;
            }
            bossWaveValue = v;
            DifficultyController.Instance.SetBossWaveExternal(bossWaveValue);
            errorText.text = "";
            Debug.Log($"BossWave set to {bossWaveValue}");
        }
        else
        {
            errorText.text = "请输入有效整数";
        }
    }

    // MaxWave 的结束编辑处理：解析并保存
    private void OnMaxWaveEndEdit(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            errorText.text = "最大波数不能为空";
            return;
        }

        if (int.TryParse(s, out int v))
        {
            if (v <= 0)
            {
                errorText.text = "最大波数必须为正整数";
                return;
            }
            maxWaveValue = v;
            DifficultyController.Instance.SetMaxWaveExternal(maxWaveValue);
            errorText.text = "";
            Debug.Log($"MaxWave set to {maxWaveValue}");
        }
        else
        {
            errorText.text = "请输入有效整数";
        }
    }

    private void OnDestroy()
    {
        bossWaveInput.onValidateInput -= ValidateDigitOnly;
        bossWaveInput.onEndEdit.RemoveListener(OnBossWaveEndEdit);
        bossWaveInput.onValueChanged.RemoveListener(OnValueChanged);

        maxWaveInput.onValidateInput -= ValidateDigitOnly;
        maxWaveInput.onEndEdit.RemoveListener(OnMaxWaveEndEdit);
        maxWaveInput.onValueChanged.RemoveListener(OnValueChanged);
    }
}
