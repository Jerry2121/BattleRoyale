using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGameConsole {

    public abstract class ConsoleCommandAction : ScriptableObject
    {

        public string[] keywords;
        [TextArea(0, 3)]
        public string helpDescription;

        public abstract void RespondToInput(ConsoleController _consoleController, string[] _separatedInputWords);
    }
}
