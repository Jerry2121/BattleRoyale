using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/ScreenFullscreen")]
public class ScreenFullscreenConsole : ConsoleCloseConsole {

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
            _consoleController.LogStringWithReturn("Switched to fullscreen");
        else
            _consoleController.LogStringWithReturn("Switched off fullscreen");
    }

}
