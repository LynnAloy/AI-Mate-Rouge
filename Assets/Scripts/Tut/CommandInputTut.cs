using experimental;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

namespace Tut
{
    public class CommandInputTut : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject controlledObj;

        private CommandParserTut parser;
        private InterpreterTut interpreter = new();

        private void Start()
        {
            parser = new CommandParserTut();
            inputField.onSubmit.AddListener(OnCommandSubmitted);
        }

        private void OnCommandSubmitted(string command)
        {
            Node node = parser.Parse(command);
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