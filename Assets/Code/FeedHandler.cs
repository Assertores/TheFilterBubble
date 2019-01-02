using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WindowHandler))]
public class FeedHandler : MonoBehaviour {

    [SerializeField] Sprite[] Icons;
    [SerializeField] float StartDelay;
    [SerializeField] float AdaptiveStartDuration = 20f;
    [SerializeField] float AdaptivePressure = 1f;
    [SerializeField] float AdaptiveForgiveness = 0.7f;
    [SerializeField] float AdaptiveMinDuration = 2f;

    float CurrentDuration;
    float nextFeed;

    WindowHandler windowHandler;

    private void Start() {
        CurrentDuration = AdaptiveStartDuration;
        nextFeed = Time.timeSinceLevelLoad + StartDelay;
        windowHandler = GetComponent<WindowHandler>();
    }

    private void Update() {
        System.Random rng = new System.Random();
        if (nextFeed < Time.timeSinceLevelLoad) {
            nextFeed += CurrentDuration;
            windowHandler.AddWindow(GameManager.GM.NewFeed().mFeed, null ,Icons[rng.Next(Icons.Length-1)]);
        }
    }
}
