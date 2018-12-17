using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGameConsole
{

    public class ConsoleTextInput : MonoBehaviour
    {

        public InputField consoleInputField;
        public string inputToAdd;

        ConsoleController consoleController;

        // Use this for initialization
        void Start()
        {
            consoleController = GetComponent<ConsoleController>();
            consoleInputField.onEndEdit.AddListener(AcceptStringInput);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void AcceptStringInput(string userInput)
        {
            if (consoleInputField.text == string.Empty)
            {
                consoleInputField.ActivateInputField();
                return;
            }

            userInput = userInput.ToLower();
            consoleController.LogStringWithReturn(userInput);


            char[] delimiterCharacters = { ' ' };
            string[] separatedInputWords = userInput.Split(delimiterCharacters);

            bool matchingCommandAction = false;

            for (int i = 0; i < consoleController.commandActions.Length; i++)
            {
                ConsoleCommandAction commandAction = consoleController.commandActions[i];

                for (int j = 0; j < commandAction.keywords.Length; j++)
                {
                    if (commandAction.keywords[j].ToLower() == separatedInputWords[j])
                    {
                        if (j == commandAction.keywords.Length - 1) //we have looped through all the keywords and they match
                        {
                            matchingCommandAction = true;
                            commandAction.RespondToInput(consoleController, separatedInputWords);
                        }
                    }
                    else
                        break;
                }
            }
            if (matchingCommandAction == false)
            {
                consoleController.LogStringWithReturn(separatedInputWords[0] + " is not a valid command");
            }

            InputComplete();
            if (inputToAdd != null)
            {
                consoleInputField.text = inputToAdd;
                //consoleController.lastInput = inputToAdd;
                inputToAdd = null;
            }
            //else
            //consoleController.lastInput = userInput;
        }

        void InputComplete()
        {
            consoleController.DisplayLoggedText();
            consoleInputField.ActivateInputField();
            consoleInputField.text = null;
        }
    }
}
