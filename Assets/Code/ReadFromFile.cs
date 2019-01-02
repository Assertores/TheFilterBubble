using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ReadFromFile {

    public static List<DataFeed> LoadFeeds(string path) {
        List<DataFeed> value = new List<DataFeed>();

        string[] FileData = File.ReadAllLines(path, System.Text.Encoding.UTF8);
        
        foreach(var it in FileData) {
            string[] LineData = it.Trim().Split("; "[0]);

            DataFeed temp = new DataFeed();

            temp.mFeed = LineData[0];
            temp.mFilterIDs = new int[LineData.Length - 1];

            for(int i = 1; i < LineData.Length; i++) {
                temp.mFilterIDs[i - 1] = int.Parse(LineData[i]);
            }

            value.Add(temp);
        }

        return value;
    }

    public static List<DataFilter> LoadFilters(string path) {
        List<DataFilter> value = new List<DataFilter>();

        string[] FileData = File.ReadAllLines(path, System.Text.Encoding.UTF8);

        foreach(var it in FileData) {
            string[] LineData = it.Trim().Split("; "[0]);

            DataFilter temp = new DataFilter();

            temp.mID = int.Parse(LineData[0]);
            temp.mFilters = new string[LineData.Length - 1];

            for(int i = 1; i < LineData.Length; i++) {
                temp.mFilters[i - 1] = LineData[i];
            }

            value.Add(temp);
        }

        return value;
    }
}
