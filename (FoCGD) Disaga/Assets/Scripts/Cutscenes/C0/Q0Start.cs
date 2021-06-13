using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q0Start : MonoBehaviour
{
    private bool canEnd = true;
    private SaveManager sm = new SaveManager();
    private TransitionManager tm;
    private TextBox textBox;
    private GameObject tbgo;

    void Start()
    {
        tm = FindObjectOfType<TransitionManager>();
        tbgo = (GameObject)Instantiate(Resources.Load("UI/TextBox"), transform.position, transform.rotation);
        textBox = FindObjectOfType<TextBox>();
        StartCoroutine(Plot());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S) && canEnd)
        {
            canEnd = false;
            EndCutscene();
        }
    }

    IEnumerator Plot()
    {
        GameObject bg = (GameObject)Instantiate(Resources.Load("Backgrounds/White"), transform.position, transform.rotation);
        GameObject SunNClouds = (GameObject)Instantiate(Resources.Load("CutsceneObjects/SunNClouds"), transform.position, transform.rotation);
        yield return new WaitUntil(() => textBox.ready);//Concurency issues?
        tm.LoadEnd();
        yield return new WaitForSeconds(1.3f);
        textBox.BeginStory("Got Transported to another world");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("This is nice. Ill get to kick some ass.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("Better become op quick.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("");
        yield return new WaitUntil(() => textBox.ready);
        SunNClouds.GetComponent<Animator>().SetTrigger("GoUp");
        yield return new WaitForSeconds(2f);
        textBox.Type("Let's go to the tutorial woods.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("END THE STORY");
        yield return new WaitUntil(() => textBox.ready);

        EndCutscene();
    }

    private void EndCutscene()
    {
        SaveData sd = new SaveData().Load();

        Quest q = sm.FindQuest(sd.quest);

        sm.SetQuestStatus(q.id, 2);
        sd.scene = 2;
        sd.Save();
        tm.LoadScene(sd.Load().scene);
    }
}