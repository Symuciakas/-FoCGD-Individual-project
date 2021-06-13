using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HostileNPC : NPC
{

    public string background;
    public int[] enemyIds;
    public int[] enemyLvls;
    public string cutsceneBefore;
    public string cutsceneAfter;
    public HostileNPC()
    {
        id = 1001;
        name = "Stupid Grunt";
        lines = new string[] { "Hurrr durr...", "Battle" };
        position = new Vector2(10, -4);
        enemyIds = new int[] { 1001 };
        enemyLvls = new int[] { 1 };
        background = "ForestBattle1";
        cutsceneBefore = "null";
        cutsceneAfter = "null";
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/NPCData/HostileNPCs.json", JsonUtility.ToJson(this));
    }

    /*public HostileNPC Load()
    {
        return JsonUtility.FromJson<HostileNPC>(File.ReadAllText(Application.streamingAssetsPath + "/NPCData/HostileNPCs.json"));
    }*/
}