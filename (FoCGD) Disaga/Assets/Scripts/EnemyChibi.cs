using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChibi : MonoBehaviour
{
    //Game data
    private TransitionManager tm;
    private SaveManager sm = new SaveManager();
    private TextBox textBox;

    //Chibi data
    private CircleCollider2D enemyRange;

    //BattleData
    private HostileNPC hostileNPC;

    void Start()
    {
        tm = FindObjectOfType<TransitionManager>();
        textBox = FindObjectOfType<TextBox>();
        enemyRange = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChibiController c = collision.GetComponent<ChibiController>();
        if (c != null)
        {
            c.SetEnabled(false);
            StartCoroutine(Plot(c));
        }
    }

    IEnumerator Plot(ChibiController c)
    {
        yield return new WaitUntil(() => textBox.ready);
        textBox.BeginStory(hostileNPC.lines[0]);
        yield return new WaitForSeconds(0.6f);
        for (int i = 1; i < hostileNPC.lines.Length; i++)
        {
            yield return new WaitUntil(() => textBox.ready);
            textBox.LoadLine(hostileNPC.lines[i]);
        }
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("END THE STORY");
        yield return new WaitUntil(() => textBox.ready);

        //Save Battle Data
        new BattleData(hostileNPC.id, hostileNPC.background, hostileNPC.enemyIds, hostileNPC.enemyLvls).Save();

        if (hostileNPC.cutsceneBefore != "null")
        {
            //Should not happen

            /*SaveData sd = new SaveData().Load();
            sd.scene = 1;
            sd.cutscene = hostileNPC.cutsceneBefore;
            sd.position = c.transform.position;
            sd.Save();*/

            //tm.LoadScene(1);
            print("NOT IMPLEMENTED!");
            print("Cutscene before battle loaded");
        }
        else
        {
            SaveData sd = new SaveData().Load();
            sd.position = c.transform.position;
            sd.scene = 3;
            sd.Save();
            tm.LoadScene(sd.Load().scene);
        }
    }

    public void SetHostileNPC(HostileNPC h)
    {
        hostileNPC = h;
    }

    /*public void SetData(string s, string bg, string ids, string l)
    {
        this.ids = ids;
        levels = l;
        line = s;
        background = bg;
    }

    private void SaveBattleData()
    {
        new BattleData(background, ids, levels).SaveBattleData();
    }*/
}