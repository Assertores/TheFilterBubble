using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Create : MonoBehaviour {

    [Header("Dependencies")]
    [SerializeField] private Text message;
    [SerializeField] Image img;
    [SerializeField] private RectTransform Frame;

    [Header("Settings")]
    [SerializeField] private float MinSize = 50;
    [SerializeField] private float UpperPadding = 0;
    [SerializeField] private float LowerPadding = 0;

    [SerializeField] private bool AutoHightAtStart = true;

    private void Start() {
        if(AutoHightAtStart)
            Create(Frame.transform.position,message.text);
    }

    private void Update() {
        if(Frame.transform.position.y < -500) {
            GetComponentInParent<WindowHandler>().DeleteWindows(gameObject);
        }
    }

    public float Create(Vector2 StartPosition, string messageString, Sprite newImg = null) {
        message.text = messageString;
        if (newImg)
            img.sprite = newImg;

        float hight = (message.preferredHeight < MinSize ? MinSize : message.preferredHeight) + UpperPadding + LowerPadding;

        Frame.sizeDelta = new Vector2(Frame.sizeDelta.x, hight);
        //Frame.position = StartPosition;

        return hight;
    }
}
