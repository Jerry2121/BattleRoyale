using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/DevEcho")]
public class DevEchoConsole : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        _consoleController.LogStringWithReturn(string.Join(" ", _separatedInputWords.Skip(2).ToArray()));
    }

}
