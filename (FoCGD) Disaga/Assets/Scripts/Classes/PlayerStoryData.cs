using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStoryData
{
    public int storyIndex;
    public int storyPart;

    public PlayerStoryData()
    {
        storyIndex = 0;
        storyPart = 1;
    }

    public PlayerStoryData(int si, int sp)
    {
        storyIndex = si;
        storyPart = sp;
    }

    public void SetStoryInfo()
    {
        string s = JsonUtility.ToJson(this);
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerInfo/StoryData.json", s);
    }

    public PlayerStoryData GetStoryData()
    {
        return JsonUtility.FromJson<PlayerStoryData>(File.ReadAllText(Application.streamingAssetsPath + "/PlayerInfo/StoryData.json"));
    }
}