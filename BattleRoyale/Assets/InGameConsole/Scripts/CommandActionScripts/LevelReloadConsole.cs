using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InGameConsole;

[CreateAssetMenu(menuName = "Console/CommandActions/LevelReload")]
public class LevelReloadConsole : ConsoleCommandAction
{

    public override void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
