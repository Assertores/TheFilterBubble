using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filter_Search : MonoBehaviour {

    [SerializeField] InputField inputField;
    [SerializeField] WindowHandler statsWindows;
    [SerializeField] GameObject Correct;
    [SerializeField] GameObject False;

    public void SerachButton() {
        if (inputField.text == "")
            return;

        if (inputField.text.StartsWith("/")) {
            GameManager.IC.InterpretCommand(inputField.text);
        }else if (GameManager.GM.SerachForFilter(inputField.text)) {
            statsWindows.AddWindow("you found the Filter " + inputField.text + " and it has been removed", Correct);//===== ===== Static String ===== =====
        } else {
            statsWindows.AddWindow(inputField.text + " was no Filter", False);//===== ===== Static String ===== =====
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

}
