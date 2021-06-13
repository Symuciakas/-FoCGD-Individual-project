using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public SaveManager()
    {

    }

    public void SetVolume(float f)
    {
        PlayerPrefs.SetFloat("MASTER_VOLUME", f);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("MASTER_VOLUME", 1f);
    }

    public Action[] FindActions(int[] ids)
    {
        Action[] actions = new Action[ids.Length];
        int aI = 0;
        foreach (int id in ids)
        {
            if (id != -1)
            {
                Action a = FindAction(id);
                if (a.id == id)
                {
                    actions[aI] = a;
                    aI++;
                }
            }
        }
        return actions;
    }

    public Action FindAction(int id)
    {
        if (id == -1)
        {
            return null;
        }
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/ActionData/Actions.json");
        int n = lines.Length;
        for (int i = 0; i < n; i++)
        {
            Action a = JsonUtility.FromJson<Action>(lines[i]);
            if (id == a.id)
            {
                return a;
            }
        }
        return null;
    }

    public Quest FindQuest(int id)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/QuestData/Quests.json");
        foreach (string s in lines)
        {
            Quest q = JsonUtility.FromJson<Quest>(s);
            if (q.id == id)
            {
                return q;
            }
        }
        return null;
    }

    public void SetQuestStatus(int id, int status)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/QuestData/Quests.json");
        for (int i = 0; i < lines.Length; i++)
        {
            Quest q = JsonUtility.FromJson<Quest>(lines[i]);
            if (q.id == id)
            {
                q.status = status;
                lines[i] = JsonUtility.ToJson(q);
            }
        }
        File.WriteAllLines(Application.streamingAssetsPath + "/QuestData/Quests.json", lines);
    }

    public void SetQuest(Quest quest)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/QuestData/Quests.json");
        for (int i = 0; i < lines.Length; i++)
        {
            Quest q = JsonUtility.FromJson<Quest>(lines[i]);
            if (q.id == quest.id)
            {
                q = quest;
                lines[i] = JsonUtility.ToJson(q);
            }
        }
        File.WriteAllLines(Application.streamingAssetsPath + "/QuestData/Quests.json", lines);
    }

    public HostileNPC FindHostileNPC(int id)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/NPCData/HostileNPCs.json");
        foreach (string s in lines)
        {
            HostileNPC h = JsonUtility.FromJson<HostileNPC>(s);
            if (h.id == id)
            {
                return h;
            }
        }
        return null;
    }

    public Character FindAlly(int id)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/CharacterData/Allies.json");
        int n = lines.Length;
        for (int i = 0; i < n; i++)
        {
            Character character = JsonUtility.FromJson<Character>(lines[i]);
            if (character.id == id)
            {
                return character;
            }
        }
        return null;
    }

    public Character FindEnemy(int id)
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/CharacterData/Enemies.json");
        int n = lines.Length;
        for (int i = 0; i < n; i++)
        {
            Character character = JsonUtility.FromJson<Character>(lines[i]);
            if (character.id == id)
            {
                return character;
            }
        }
        return null;
    }

    public Character[] GetAllyCharacters()
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/CharacterData/Allies.json");
        int n = lines.Length;
        Character[] characters = new Character[n];
        for (int i = 0; i < n; i++)
        {
            characters[i] = JsonUtility.FromJson<Character>(lines[i]);
        }
        return characters;
    }

    public Character[] GetEnemyCharacters()
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/CharacterData/Enemies.json");
        int n = lines.Length;
        Character[] characters = new Character[n];
        for (int i = 0; i < n; i++)
        {
            characters[i] = JsonUtility.FromJson<Character>(lines[i]);
        }
        return characters;
    }
}