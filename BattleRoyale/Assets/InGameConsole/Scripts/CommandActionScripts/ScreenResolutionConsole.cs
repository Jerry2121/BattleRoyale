using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/ScreenResolution")]
public class ScreenResolutionConsole : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        int num;
        bool failure = false;
        bool fullscreen = false;

        if (int.TryParse(_separatedInputWords[2], out num) == false){
            _consoleController.LogStringWithReturn(_separatedInputWords[2] + " is not a valid number");
            failure = true;
        }
        if (int.TryParse(_separatedInputWords[3], out num) == false)
        {
            _consoleController.LogStringWithReturn(_separatedInputWords[3] + " is not a valid number");
            failure = true;
        }
        if(_separatedInputWords[4] != "true" && _separatedInputWords[4] != "false"){
            _consoleController.LogStringWithReturn(_separatedInputWords[4] + " is not a valid input. the fifth input should be either 'true' or 'false'");
            failure = true;
        }

        if (failure)
        {
            Debug.Log("ScreenResolution -- RespondToInput: Failed to change the screen resoultion");
            return;
        }

        if(_separatedInputWords[4] == "true")
        {
            fullscreen = true;
        }
        Screen.SetResolution(int.Parse(_separatedInputWords[2]), int.Parse(_separatedInputWords[3]), fullscreen);
    }

}
