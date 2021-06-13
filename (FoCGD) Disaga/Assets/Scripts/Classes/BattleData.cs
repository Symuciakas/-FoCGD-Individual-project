using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleData
{
    public int enemyChibiId;
    public string background;
    public int[] enemyIds;
    public int[] enemyLvls;

    public BattleData(int ecid, string bg, int[] ids, int[] lvls)
    {
        enemyChibiId = ecid;
        background = bg;
        enemyIds = ids;
        enemyLvls = lvls;
    }

    public BattleData()
    {

    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerData/BattleData.json", JsonUtility.ToJson(this));
    }

    public BattleData Load()
    {
        return JsonUtility.FromJson<BattleData>(File.ReadAllText(Application.streamingAssetsPath + "/PlayerData/BattleData.json"));
    }
}