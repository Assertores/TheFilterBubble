using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataFilter {
    public int mID;
    public string[] mFilters;
}

[System.Serializable]
public class DataFeed {
    public string mFeed = "";
    public int[] mFilterIDs;
}

public class GameManager : MonoBehaviour {

    public static GameManager GM = null;

    [SerializeField] GameObject YouWon;

    [SerializeField] int StartFilterAmount = 5;
    [SerializeField] string FeedPath;
    [SerializeField] string FilterPath;


    [Header("Debug")]
    [SerializeField]  List<DataFeed> AllFeeds = new List<DataFeed>();
    [SerializeField]  List<DataFilter> AllFilters = new List<DataFilter>();

    [SerializeField]  List<DataFilter> CurrentFilters = new List<DataFilter>();


    void Awake() {
        if (GM) {
            Destroy(this);
        } else {
            GM = this;
        }


        AllFeeds = ReadFromFile.LoadFeeds(Application.streamingAssetsPath + "/" + FeedPath);
        AllFilters = ReadFromFile.LoadFilters(Application.streamingAssetsPath + "/" + FilterPath);

        restart();
    }

    private void OnDestroy() {
        if(GM == this) {
            GM = null;
        }
    }

    public void restart() {

        foreach(var it in GetComponentsInChildren<WindowHandler>()) {
            it.DeleteWindows();
        }


        ShuffleAllFilters();
        for (int i = 0; i < StartFilterAmount && i < AllFilters.Count; i++) {
            bool newOne = true;
            for (int j = 0; j < CurrentFilters.Count; j++) {
                if (AllFilters[i].mID == CurrentFilters[j].mID) {
                    newOne = false;
                }
            }
            if (newOne)
                CurrentFilters.Add(AllFilters[i]);
        }

        ShuffleAllFeeds();
        YouWon.SetActive(false);
        Time.timeScale = 1;
    }

    void ShuffleAllFilters() {
        System.Random rng = new System.Random();
        for (int i = 0; i < AllFilters.Count; i++) {
            var tmp = AllFilters[i];
            int r = rng.Next(i, AllFilters.Count);
            AllFilters[i] = AllFilters[r];
            AllFilters[r] = tmp;
        }
    }

    void ShuffleAllFeeds() {
        System.Random rng = new System.Random();
        for (int i = 0; i < AllFeeds.Count; i++) {
            var tmp = AllFeeds[i];
            int r = rng.Next(i, AllFeeds.Count);
            AllFeeds[i] = AllFeeds[r];
            AllFeeds[r] = tmp;
        }
    }

    public DataFeed NewFeed() {
        DataFeed temp = new DataFeed();

        ShuffleAllFeeds();

        bool ready = false;
        for (int m = 0; !ready && m < AllFeeds.Count; m++) {
            ready = true;
            for (int i = 0; ready && i < AllFeeds[m].mFilterIDs.Length; i++) {
                for (int j = 0; ready && j < CurrentFilters.Count; j++) {
                    if (AllFeeds[m].mFilterIDs[i] == CurrentFilters[j].mID) {
                        ready = false;
                    }
                }
            }
            if (ready) {
                temp = AllFeeds[m];
            }
        }

        return temp;
    }

    public bool SerachForFilter(string serach) {
        bool value = false;

        for(int i = 0; !value && i < CurrentFilters.Count; i++) {
            for(int j = 0; !value && j < CurrentFilters[i].mFilters.Length; j++) {
                if(CurrentFilters[i].mFilters[j] == serach) {
                    value = true;
                    CurrentFilters.Remove(CurrentFilters[i]);
                    if(CurrentFilters.Count == 0) {
                        YouWon.SetActive(true);
                        Time.timeScale = 0;
                    }
                }
            }
        }

        return value;
    }
}
