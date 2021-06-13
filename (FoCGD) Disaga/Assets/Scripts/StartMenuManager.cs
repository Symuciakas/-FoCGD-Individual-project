using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartMenuManager : MonoBehaviour
{
    private bool reset = false;
    private TransitionManager tm;

    void Start()
    {
        tm = FindObjectOfType<TransitionManager>();

        tm.LoadEnd();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R) && !reset)
        {
            ResetSaveData();
        }
    }

    public void StartClick()
    {
        tm.LoadScene(new SaveData().Load().scene);
    }

    private void ResetSaveData()
    {
        //Copy files from originals
        new SaveManager().SetVolume(1f);
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerData/SaveData.json", File.ReadAllText(Application.streamingAssetsPath + "/OriginalStartFiles/PlayerData/SaveData.json"));
        File.WriteAllText(Application.streamingAssetsPath + "/QuestData/Quests.json", File.ReadAllText(Application.streamingAssetsPath + "/OriginalStartFiles/QuestData/Quests.json"));
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerData/PartyData.json", File.ReadAllText(Application.streamingAssetsPath + "/OriginalStartFiles/PlayerData/PartyData.json"));
    }
}