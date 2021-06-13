using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public int scene;
    public Vector2 position;
    public int quest;
    public string cutscene;
    public int[] quests;

    public SaveData()
    {
        scene = 0;
        position = new Vector2(0, 0);
        quest = 0;
        cutscene = "null";
        quests = new int[] { 0 };
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerData/SaveData.json", JsonUtility.ToJson(this));
    }

    public SaveData Load()
    {
        return JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.streamingAssetsPath + "/PlayerData/SaveData.json"));
    }
}