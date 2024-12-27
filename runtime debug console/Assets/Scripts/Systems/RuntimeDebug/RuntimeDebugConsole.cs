using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeDebugConsole : Singleton<RuntimeDebugConsole>
{
    [SerializeField] TMP_InputField debugCommandEntry;
    [SerializeField] TMP_Text debugLogEntry;
    [SerializeField] ScrollRect debugLogScroll;

    private Queue<string> logQueue = new Queue<string>();
    private int maxLogCount = 50;

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
        debugCommandEntry.AddInputFieldTMPSubmitListener(ExecuteCommand);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        logQueue.Enqueue(logString);

        if (logQueue.Count > maxLogCount)
        {
            logQueue.Dequeue();
        }

        debugLogEntry.text = string.Join("\n", logQueue.Reverse());
        Canvas.ForceUpdateCanvases();
        debugLogScroll.verticalNormalizedPosition = 0;

    }

    public void ExecuteCommand()
    {
        string command = debugCommandEntry.text.Trim();
        debugCommandEntry.text = "";

        if (string.IsNullOrEmpty(command)) return;

        Debug.Log($">{command}");

        DebugProcessor.Instance.ExecuteCommand(command);
    }
}
