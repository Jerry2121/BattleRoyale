using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/ConsoleClose")]
public class ConsoleCloseConsole : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        _consoleController.ToggleConsole();
    }

}
