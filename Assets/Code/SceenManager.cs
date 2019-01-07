using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.IC.WriteToConsole("===== ===== Sceenmanager is ready ===== ===== dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddA");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Quit();
        }
    }

    public void Quit() {
        Application.Quit();
    }
}
