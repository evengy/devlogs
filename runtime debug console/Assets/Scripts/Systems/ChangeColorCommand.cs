using UnityEngine;

public class ChangeColorCommand : IDebugCommand
{
    public void ExecuteDebugCommand(string[] args)
    {
        Debug.Log($"Test Command Test");

        if (args[0] == "red")
        {
            ImageWithSwappableColorSystem.Instance.ChangeImageColor(Color.red);
        }

        if (args[0] == "blue")
        {
            ImageWithSwappableColorSystem.Instance.ChangeImageColor(Color.blue);
        }
    }
}
