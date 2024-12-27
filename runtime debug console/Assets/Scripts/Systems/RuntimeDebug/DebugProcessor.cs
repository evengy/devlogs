using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugProcessor : Singleton<DebugProcessor>
{
    private readonly Dictionary<string, IDebugCommand> commands = new Dictionary<string, IDebugCommand>();

    private void Awake()
    {
        var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IDebugCommand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

        foreach (var type in commandTypes)
        {
            string commandName = type.Name.Replace("Command", "").ToLower();
            IDebugCommand commandInstance = (IDebugCommand)Activator.CreateInstance(type);
            RegisterCommand(commandName, commandInstance);
        }
    }

    private void RegisterCommand(string commandName, IDebugCommand command)
    {
        if (!commands.ContainsKey(commandName))
        {
            commands.Add(commandName, command);
        }
    }

    public void ExecuteCommand(string commandLine)
    {
        if (string.IsNullOrWhiteSpace(commandLine)) return;

        string[] parts = commandLine.Split(' ');
        string commandName = parts[0].ToLower();
        string[] args = parts.Length > 1 ? parts[1..] : Array.Empty<string>();

        if (commands.TryGetValue(commandName, out IDebugCommand command))
        {
            try
            {
                command.ExecuteDebugCommand(args);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error executing command '{commandName}': {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Unknown command: {commandName}");
        }
    }
}
