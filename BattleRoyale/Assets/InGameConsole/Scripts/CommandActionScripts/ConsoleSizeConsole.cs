using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/ConsoleSize")]
public class ConsoleSizeConsole : ConsoleCommandAction
{
    
    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        int num;
        bool failure = false;

        if (int.TryParse(_separatedInputWords[2], out num) == false)
        {
            _consoleController.LogStringWithReturn(_separatedInputWords[2] + " is not a valid number");
            failure = true;
        }
        if (int.TryParse(_separatedInputWords[3], out num) == false)
        {
            _consoleController.LogStringWithReturn(_separatedInputWords[3] + " is not a valid number");
            failure = true;
        }

        if (failure)
        {
            Debug.Log("ConsoleSize -- RespondToInput: Failed to change the console size");
            return;
        }

        RectTransform consoleTransform = (RectTransform)_consoleController.transform;
        consoleTransform.sizeDelta = new Vector2(int.Parse(_separatedInputWords[2]), int.Parse(_separatedInputWords[3]));
    }

}
