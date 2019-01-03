using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;
using System.IO;

[CreateAssetMenu(menuName = "Console/CommandActions/ConsoleLog")]
public class ConsoleLogConsole : ConsoleCommandAction {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        string path;
        if (_separatedInputWords.Length > 2)
            path = _separatedInputWords[2] + ".txt";
        else
            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\consoleLog.txt";

        List<string> loggedText = _consoleController.actionLog;

        string[] loggedTextArray = loggedText.ToArray();

        string[] createText = (loggedTextArray);
        File.WriteAllLines(path, createText);

        _consoleController.LogStringWithReturn("Console history logged to " + path);
    }

}
