using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnlockedCharacter
{
    public int id;
    public int level;
    public int exp;
    public int[] neededExp;
    public int bond;

    public UnlockedCharacter()
    {
        id = 0;
        level = 1;
        exp = 0;
        neededExp = new int[] { 100, 200, 600, 2400, 12000, 72000, 576000, 5184000, 51840000 };
        bond = -1;
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/CharacterData/UnlockedCharacterData.json", JsonUtility.ToJson(this));
    }

    public BattleData Load()
    {
        return JsonUtility.FromJson<BattleData>(File.ReadAllText(Application.streamingAssetsPath + "/CharacterData/UnlockedCharacterData.json"));
    }
}