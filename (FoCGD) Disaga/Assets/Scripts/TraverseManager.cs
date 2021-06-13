using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TraverseManager : MonoBehaviour
{
    private TransitionManager tm;
    private SaveManager sm = new SaveManager();
    public ChibiController chibiController;
    private TextBox textBox;
    private GameObject tilemap;
    private Quest quest;

    private bool canLoad = true;

    void Start()
    {
        tm = FindObjectOfType<TransitionManager>();
        GameObject tbgo = (GameObject)Instantiate(Resources.Load("UI/TextBox"), transform.position, transform.rotation);
        textBox = FindObjectOfType<TextBox>();
        SaveData sd = new SaveData().Load();
        quest = sm.FindQuest(sd.quest);
        if (quest != null)//Check no quest?
        {
            //Return quest map
            tilemap = (GameObject)Instantiate(Resources.Load("Tilemaps/" + quest.mapId), transform.position, transform.rotation);
        }
        else
        {
            //No quest. Return base map
            //tilemap = (GameObject)Instantiate(Resources.Load("Tilemaps/" + -1), transform.position, transform.rotation);

            print("NOT IMPLEMENTED!");
            print("Main map loaded");
        }
        PopulateNPCs();

        chibiController.transform.position = sd.position;

        tm.LoadEnd();

        chibiController.EnableDelayed(1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canLoad)
        {
            canLoad = false;
            tm.LoadScene(0);
        }
    }

    void PopulateNPCs()
    {
        //do iterator for npcs later

        //Instantiate enemies
        foreach (int i in quest.enemies)
        {

            HostileNPC hNPC = sm.FindHostileNPC(i);
            GameObject g = (GameObject)Instantiate(Resources.Load("EnemyChibis/" + hNPC.id), hNPC.position, transform.rotation);
            g.name = hNPC.id.ToString();
        }
        //Instantiate id enemies
        foreach (EnemyChibi ec in FindObjectsOfType<EnemyChibi>())
        {
            ec.SetHostileNPC(sm.FindHostileNPC(int.Parse(ec.name)));
        }
    }
}