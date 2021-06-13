using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PartyData
{
    public int[] ids;
    public int[] levels;

    public PartyData()
    {
        ids = new int[] { 0 };
        levels = new int[] { 1 };
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerData/PartyData.json", JsonUtility.ToJson(this));
    }

    public PartyData Load()
    {
        return JsonUtility.FromJson<PartyData>(File.ReadAllText(Application.streamingAssetsPath + "/PlayerData/PartyData.json"));
    }
}