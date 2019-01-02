using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour {

    [SerializeField] GameObject PrefabWindow;
    [SerializeField] RectTransform StartingPosition;
    [SerializeField] string Text = "No Text Avaliable";

    List<GameObject> Windows = new List<GameObject>();

    public void AddWindow(string message = "", GameObject window = null, Sprite img = null) {
        Windows.Add(Instantiate(window ? window : PrefabWindow,this.transform));

        float NewWindowHight = Windows[Windows.Count - 1].GetComponent<Window_Create>().Create(StartingPosition.position, message.Length != 0 ? message : Text, img);
        foreach (var it in Windows) {
            it.GetComponent<Window_Move>().MoveDown(NewWindowHight);
        }
    }

    public void DeleteWindows(GameObject window = null) {
        if (!window) {
            foreach(var it in Windows) {
                Destroy(it);
            }
            Windows.Clear();
        } else {
            Destroy(window);
            Windows.Remove(window);
        }
    }
}
