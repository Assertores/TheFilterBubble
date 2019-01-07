using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class ReadFromFile {

    public static List<DataFeed> LoadFeeds(string path) {
        List<DataFeed> value = new List<DataFeed>();

        string[] FileData = FileToLineArray(path);
        
        foreach(var it in FileData) {
            string[] LineData = it.Trim().Split(";"[0]);

            DataFeed temp = new DataFeed();

            temp.mFeed = LineData[0];
            temp.mFilterIDs = new int[LineData.Length - 1];

            for(int i = 1; i < LineData.Length; i++) {
                GameManager.IC.WriteToConsole("beforde int cast " + LineData[i]);//===== ===== LOG ===== =====
                if(!System.Int32.TryParse(LineData[i], out temp.mFilterIDs[i - 1])) {
                    GameManager.IC.WriteToConsole("i failed");//===== ===== LOG ===== =====
                    temp.mFilterIDs[i - 1] = 0;
                }
                GameManager.IC.WriteToConsole("after int cast " + temp.mFilterIDs[i-1]);//===== ===== LOG ===== =====
            }

            value.Add(temp);
        }

        return value;
    }

    public static List<DataFilter> LoadFilters(string path) {
        List<DataFilter> value = new List<DataFilter>();

        string[] FileData = FileToLineArray(path);

        foreach(var it in FileData) {
            string[] LineData = it.Trim().Split(";"[0]);

            DataFilter temp = new DataFilter();
            GameManager.IC.WriteToConsole("beforde int cast " + LineData[0]);//===== ===== LOG ===== =====
            if(!System.Int32.TryParse(LineData[0], out temp.mID)) {
                GameManager.IC.WriteToConsole("i failed");//===== ===== LOG ===== =====
                temp.mID = 0;
            }
            GameManager.IC.WriteToConsole("after int cast " + temp.mID);//===== ===== LOG ===== =====
            temp.mFilters = new string[LineData.Length - 1];

            for(int i = 1; i < LineData.Length; i++) {
                temp.mFilters[i - 1] = LineData[i];
            }

            value.Add(temp);
        }

        return value;
    }

    static string[] FileToLineArray(string path) {
        string[] FileData = null;

        GameManager.IC.WriteToConsole("reading in " + path);//===== ===== LOG ===== =====
        if (Application.platform == RuntimePlatform.Android) {
            //GameManager.IC.WriteToConsole("jar:file://" + Application.dataPath + "!/assets/" + path);//===== ===== LOG ===== =====
            //UnityWebRequest reader = new UnityWebRequest("jar:file://" + Application.dataPath + "!/assets/" + path);
            //if(reader.isNetworkError || reader.isHttpError) {
            //    GameManager.IC.WriteToConsole("i had an error: " + reader.error);
            //    return new string[1] { reader.error };
            //}

            //string temp = reader.downloadHandler.text;
            //GameManager.IC.WriteToConsole(temp);//===== ===== LOG ===== =====
            //FileData = temp.Split(System.Environment.NewLine[0]);
            GameManager.IC.WriteToConsole(Application.streamingAssetsPath + "/" + path);//===== ===== LOG ===== =====
            WWW reader = new WWW(Application.streamingAssetsPath + "/" + path);
            /*if (reader.error == "") {
                GameManager.IC.WriteToConsole("i had an error: " + reader.error);
                return new string[1] { reader.error };
            }*/

            string temp = reader.text;
            FileData = temp.Split(System.Environment.NewLine[0]);

            reader.Reset();
        } else {
            GameManager.IC.WriteToConsole(Application.streamingAssetsPath + "/" + path);//===== ===== LOG ===== =====
            FileData = File.ReadAllLines(Application.streamingAssetsPath + "/" + path, System.Text.Encoding.UTF8);
        }
        foreach(var it in FileData) {
            GameManager.IC.WriteToConsole("=" + it);//===== ===== LOG ===== =====
        }

        return FileData;
    }
}
