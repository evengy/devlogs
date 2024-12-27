using TMPro;
using UnityEngine;

public delegate void InputFieldTMPSubmitCallback();

public static class Utils
{
    public static void AddInputFieldTMPSubmitListener(this TMP_InputField inputField, InputFieldTMPSubmitCallback submitCallback)
    {
        inputField.onSubmit.RemoveAllListeners();
        inputField.onSubmit.AddListener(text =>
        {
            if (!inputField.wasCanceled)
            {
                submitCallback?.Invoke();
            }
        });
    }
}
