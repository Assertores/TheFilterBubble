using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameConsole : MonoBehaviour {

    [SerializeField] Text consoleContent;
    [SerializeField] GameObject console;

    bool flushed = false;

    public void SetConsoleVisable(bool flag) {
        console.SetActive(flag);
    }

    public void SetConsoleVisable() {
        console.SetActive(!console.activeSelf);
    }

    public void FlushConsole() {
        consoleContent.text = "";
    }

    public void WriteToConsole(string message) {
        if (!flushed) {
            consoleContent.text = "";
            flushed = true;
        } else if (consoleContent != null) {
            consoleContent.text += System.Environment.NewLine;
        }
        if(consoleContent != null) {
            consoleContent.text += message;
            print(message);
            consoleContent.rectTransform.sizeDelta = new Vector2(consoleContent.rectTransform.sizeDelta.x, consoleContent.preferredHeight);
        }
    }

    public bool InterpretCommand(string value) {
        value = value.Remove(0, 1);
        if (value.Contains("ShowConsole") || value.Contains("showConsole")) {
            SetConsoleVisable(true);
            return true;
        }
        if (value.Contains("HideConsole") || value.Contains("hideConsole")) {
            SetConsoleVisable(false);
            return true;
        }
        if (value.Contains("Console") || value.Contains("console")) {
            SetConsoleVisable();
            return true;
        }
        return false;
    }
}
