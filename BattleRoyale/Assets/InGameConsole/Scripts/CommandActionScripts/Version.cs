using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/Version")]
public class Version : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        _consoleController.LogStringWithReturn("version " + Application.version);
    }

}
