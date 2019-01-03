using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/Quit")]
public class QuitGameConsole : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        Application.Quit();
    }

}
