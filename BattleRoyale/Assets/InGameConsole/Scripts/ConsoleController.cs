using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGameConsole {

    public class ConsoleController : MonoBehaviour
    {

        public Text logText;
        public GameObject consoleView;
        public KeyCode consoleToggleKey = KeyCode.BackQuote;
        public bool displayStackTrace = true;
        public ConsoleCommandAction[] commandActions;

        public List<string> actionLog = new List<string>();
        public List<string> inputHistory = new List<string>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(consoleToggleKey))
            {
                ToggleConsole();
            }
        }

        public void DisplayLoggedText()
        {
            List<string> logAsTextList = actionLog;

            int num = logAsTextList.Count;
            for (int i = 0; i < num - 24; i++)
            {
                logAsTextList.Remove(logAsTextList[i]);
            }

            string logAsText = string.Join("\n", logAsTextList.ToArray());

            logText.text = logAsText;
        }

        public void LogStringWithReturn(string stringToAdd)
        {
            actionLog.Add(stringToAdd + "\n");
        }
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            string colorString = string.Empty;
            if (type == LogType.Error)
                colorString = "red";
            else if (type == LogType.Warning)
                colorString = "yellow";
            else
                colorString = "white";


            LogStringWithReturn("<color=" + colorString + ">" + logString + "</color>");
            if (displayStackTrace)
                LogStringWithReturn("<color=" + colorString + ">" + stackTrace + "</color>");

            DisplayLoggedText();

            if (consoleView.activeSelf == false && type == LogType.Error)
                consoleView.SetActive(true);
        }

        public void ToggleConsole()
        {
            consoleView.SetActive(!consoleView.activeSelf);
        }
    }
}
