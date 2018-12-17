using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;

    [CreateAssetMenu(menuName = "Console/CommandActions/Help")]
    public class Help : ConsoleCommandAction
    {

        public override void RespondToInput(ConsoleController _controller, string[] _separatedInputWords)
        {
            if (_separatedInputWords.Length == 2)
            {
                _controller.LogStringWithReturn("These are some " + _separatedInputWords[1] + " commands you can use:");

                for (int i = 0; i < _controller.commandActions.Length; i++)
                {
                    if (_controller.commandActions[i].helpDescription != string.Empty && _controller.commandActions[i].keywords[0].ToLower() == _separatedInputWords[1].ToLower())
                        _controller.LogStringWithReturn("- " + string.Join(" ", _controller.commandActions[i].keywords) + " - " + _controller.commandActions[i].helpDescription);
                }
            }

            if (_separatedInputWords.Length == 3)
            {
                _controller.LogStringWithReturn("These are some " + _separatedInputWords[1] + " " + _separatedInputWords[2] + " commands you can use:");

                for (int i = 0; i < _controller.commandActions.Length; i++)
                {
                    if (_controller.commandActions[i].helpDescription != string.Empty && _controller.commandActions[i].keywords[0].ToLower() == _separatedInputWords[1].ToLower() && _controller.commandActions[i].keywords[1].ToLower() == _separatedInputWords[2].ToLower())
                        _controller.LogStringWithReturn("- " + string.Join(" ", _controller.commandActions[i].keywords) + " - " + _controller.commandActions[i].helpDescription);
                }
            }

            else
            {
                _controller.LogStringWithReturn("These are some commands you can use:");

                for (int i = 0; i < _controller.commandActions.Length; i++)
                {
                    if (_controller.commandActions[i].helpDescription != string.Empty && _controller.commandActions[i].keywords[0].ToLower() != "Dev".ToLower())
                        _controller.LogStringWithReturn("- " + string.Join(" ", _controller.commandActions[i].keywords) + " - " + _controller.commandActions[i].helpDescription);
                }
            }
        }
    }
