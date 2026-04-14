using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace experimental
{
    public class CommandInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject controlledObj;
        [SerializeField] private TMP_Text warningText;

        private CommandParser parser;
        private Interpreter interpreter = new();
        public Action OnFreshKeyWordsDisplay;

        private void Start()
        {
            if (KeyWordController.Instance != null && warningText != null)
            {
                var keyWordsOwned = KeyWordController.Instance.GetKeyWordsOwned();
                parser = new CommandParser(keyWordsOwned, warningText, () => OnFreshKeyWordsDisplay?.Invoke());
            }
            else
            {
                Debug.LogError("CommandInput: KeyWordController instance or warningText is null.");
            }
            inputField.onSubmit.AddListener(OnCommandSubmitted);
        }

        private void OnCommandSubmitted(string command)
        {
            var node = parser.Parse(command);
            if (node != null)
            {
                interpreter.Execute(node, controlledObj);
            }
            else
            {
                Debug.Log("Invalid command");
            }
            inputField.text = "";
        }
    }
}