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
    public static IngameConsole IC = null;

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
            IC = FindObjectOfType<IngameConsole>();
        }

        IC.WriteToConsole("i will load the feeds now");//===== ===== LOG ===== =====
        AllFeeds = ReadFromFile.LoadFeeds(FeedPath);
        IC.WriteToConsole("i will load the filters now");//===== ===== LOG ===== =====
        AllFilters = ReadFromFile.LoadFilters(FilterPath);
        
        IC.WriteToConsole("i will restart the game now");//===== ===== LOG ===== =====
        restart();
    }

    private void OnDestroy() {
        if(GM == this) {
            GM = null;
        }
    }

    float lastacceleration = 0;
    float lastVelocity = 0;
    private void Update() {
        float currentVelocity = Input.acceleration.y - lastacceleration / Time.deltaTime;
        if(Input.GetKeyUp(KeyCode.F5) || ((currentVelocity - lastVelocity)/Time.deltaTime > 1000f && Time.timeSinceLevelLoad > 1f)) {
            IC.SetConsoleVisable();
        }
        //IC.WriteToConsole(((currentVelocity-lastVelocity)/Time.deltaTime).ToString());//===== ===== LOG ===== =====
        lastVelocity = currentVelocity;
        lastacceleration = Input.acceleration.y;
    }

    public void restart() {

        IC.WriteToConsole("restart");//===== ===== LOG ===== =====

        foreach (var it in GetComponentsInChildren<WindowHandler>()) {
            it.DeleteWindows();
        }


        ShuffleAllFilters();
        IC.WriteToConsole("Filters shuffled");//===== ===== LOG ===== =====
        for (int i = 0; i < StartFilterAmount && i < AllFilters.Count; i++) {
            bool newOne = true;
            for (int j = 0; j < CurrentFilters.Count; j++) {
                if (AllFilters[i].mID == CurrentFilters[j].mID) {
                    newOne = false;
                }
            }
            if (newOne) {
                CurrentFilters.Add(AllFilters[i]);
                IC.WriteToConsole("added " + AllFilters[i].mFilters[0] + " to the filters");//===== ===== LOG ===== =====
            }
        }

        ShuffleAllFeeds();
        IC.WriteToConsole("Feeds shuffled");//===== ===== LOG ===== =====
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

        IC.WriteToConsole("the filter serach for " + serach + " returned " + value.ToString() + ". the Active Filter Count is " + CurrentFilters.Count);

        return value;
    }
}
