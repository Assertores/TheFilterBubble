using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Move : MonoBehaviour {

    [SerializeField] private float Hight = 0;
    [SerializeField] private float Speed = 100;
    [SerializeField] private RectTransform Frame;

    Vector2 target;

    void Awake() {

        if (Hight == 0) {
            Hight = Frame.sizeDelta.y;
        }
        target = Frame.localPosition;
    }
    
    void Update() {
        if (Frame.localPosition.y > target.y) {
            Frame.localPosition -= new Vector3(0, Speed * Time.deltaTime, 0);
            if(Frame.localPosition.y < target.y) {
                Frame.localPosition = target;
            }
        }
    }

    public void MoveDown(float distance) {
        target = new Vector2(Frame.localPosition.x,Frame.localPosition.y - distance);
    }
}
