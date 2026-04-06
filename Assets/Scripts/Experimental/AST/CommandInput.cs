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

        private CommandParser parser = new();
        private Interpreter interpreter = new();

        private void Start()
        {
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