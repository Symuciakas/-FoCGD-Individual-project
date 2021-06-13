using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CutsceneManager : MonoBehaviour
{
    private GameObject cm;
    private SaveManager sm = new SaveManager();

    void Start()
    {
        SaveData sd = new SaveData().Load();
        Quest q = sm.FindQuest(sd.quest);
        //1 - Starter, 2
        if (q.status == 1)
        {
            cm = (GameObject)Instantiate(Resources.Load("CutsceneManagers/Q" + sd.quest + "Start"), transform.position, transform.rotation);
        }
        else if (q.status == 2 && sd.cutscene != "null")
        {
            cm = (GameObject)Instantiate(Resources.Load("CutsceneManagers/" + sd.cutscene), transform.position, transform.rotation);
        }
        else if (q.status == 3)
        {
            cm = (GameObject)Instantiate(Resources.Load("CutsceneManagers/Q" + sd.quest + "End"), transform.position, transform.rotation);
        }
    }
}