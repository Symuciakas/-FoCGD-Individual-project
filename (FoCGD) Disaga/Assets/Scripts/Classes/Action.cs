using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Action
{
    public int id;
    public string name;
    public string description;
    public bool harmful;
    public bool aoe;
    public int type;
    public int target;
    public float mult;
    public string[] effects;

    public Action()
    {
        id = 0;
        name = "Slash";
        description = "Basic slash. Inflicts low physical damage.";//Add effects
        harmful = true;
        aoe = false;
        type = 1;
        target = 1;
        mult = 1f;
        effects = new string[] { };//Add later
    }

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/ActionData/Actions.json", JsonUtility.ToJson(this));
    }

    public Action Load()
    {
        return JsonUtility.FromJson<Action>(File.ReadAllText(Application.streamingAssetsPath + "/ActionData/Actions.json"));
    }
}