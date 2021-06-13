using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Quest
{
    public int id;
    public string name;
    public int status;
    public int mapId;
    public int[] npcs;
    public int[] enemies;

    public Quest()
    {
        id = 0;
        name = "Tutorial";
        status = 1;
        mapId = 0;
        npcs = new int[] { };
        enemies = new int[] { 1001 };
    }

    /*public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/QuestData/Quests.json", JsonUtility.ToJson(this));
    }

    public Quest Load()
    {
        return JsonUtility.FromJson<Quest>(File.ReadAllText(Application.streamingAssetsPath + "/QuestData/Quests.json"));
    }*/
}