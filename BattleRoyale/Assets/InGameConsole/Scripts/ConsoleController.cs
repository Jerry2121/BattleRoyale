using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGameConsole {

    public class ConsoleController : MonoBehaviour
    {

        public Text logText;
        public InputField inputField;
        public GameObject consoleView;
        public GameObject enterButton;
        public KeyCode consoleToggleKey = KeyCode.BackQuote;
        public bool displayStackTrace = true;
        public bool canAcceptInput = true;
        public bool popUpOnError = true;
        public ConsoleCommandAction[] commandActions;

        public List<string> actionLog = new List<string>();
        public List<string> inputHistory = new List<string>();

        int inputHistoryNum = 1;

        // Use this for initialization
        void Start()
        {
            if(canAcceptInput == false)
            {
                inputField.gameObject.SetActive(false);
                enterButton.SetActive(false);
            }
            DontDestroyOnLoad(transform.parent);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(consoleToggleKey))
            {
                ToggleConsole();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && inputHistory.Count > 0)
            {
                inputField.text = inputHistory[inputHistory.Count - inputHistoryNum];
                inputField.caretPosition = inputField.text.Length;
                inputHistoryNum++;
                if (inputHistoryNum.CompareTo(inputHistory.Count + 1) == 0)
                    inputHistoryNum = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputField.ActivateInputField();
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
            inputHistoryNum = 1;
            actionLog.Add(stringToAdd.ToLower() + "\n");
        }
        void OnEnable()
        {
            Application.logMessageReceivedThreaded += HandleLogThreaded;
        }

        void OnDisable()
        {
            Application.logMessageReceivedThreaded -= HandleLogThreaded;
        }

        void HandleLogThreaded(string logString, string stackTrace, LogType type)
        {
            string colorString = string.Empty;
            if (type == LogType.Error || type == LogType.Exception)
                colorString = "red";
            else if (type == LogType.Warning)
                colorString = "yellow";
            else
                colorString = "white";

            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("StackFrames: " + trace.FrameCount + "\n");
            for (int i = 0; i < trace.FrameCount; i++)
            {
                System.Diagnostics.StackFrame sf = trace.GetFrame(i);
                string filePath = sf.GetFileName();
                if (filePath == null)
                {
                    sb.Append("StackFrame " + i + ": <b>" + sf.GetMethod() + "</b> (File path returned null) \n");
                    continue;
                }

                string[] filePathArray = filePath.Split('\\');
                filePathArray = filePathArray[filePathArray.Length - 1].Split('.');
                string assetPath = "<b>Error: asset path not found</b>";
                /*Type t = Type.GetType(sf.GetMethod().ReflectedType.Name);
                if(t == null)
                {
                    return;
                }*/
                //var tempInstance = Activator.CreateInstance(sf.GetMethod().DeclaringType);
                //if(tempInstance != null)
                    //assetPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.MonoScript.FromMonoBehaviour((MonoBehaviour)tempInstance));
                //assetPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.MonoScript.FromMonoBehaviour(Type.GetType(sf.GetMethod().DeclaringType.Name)));
                sb.Append("StackFrame " + i + ": <b>" + filePathArray[0] + ":" + sf.GetMethod() + "</b> at   " + assetPath + " line number <b>" + sf.GetFileLineNumber() +"</b> \n");
            }

            stackTrace = sb.ToString();

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
            if(consoleView.activeSelf == true)
            {
                inputField.ActivateInputField();
            }
        }
    }
}
