using UnityEngine;
using UnityEngine.UI;

public class CommandInputHandler : MonoBehaviour
{
    public TMPro.TMP_InputField commandInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessCommand(commandInput.text);
            commandInput.text = "";
        }
    }

    void ProcessCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length < 2) return;

        string command = parts[0].Substring(1).ToLower();
        string playerName = parts[1];
        int value = parts.Length > 2 ? int.Parse(parts[2]) : 0;

        CommandManager.Instance.ExecuteCommand(command, playerName, value);
    }
}