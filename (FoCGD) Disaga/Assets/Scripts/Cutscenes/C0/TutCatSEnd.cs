using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCatSEnc : MonoBehaviour
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
        GameObject MC = (GameObject)Instantiate(Resources.Load("CutsceneObjects/McIdle"), transform.position, transform.rotation);
        yield return new WaitUntil(() => textBox.ready);//Concurency issues?
        tm.LoadEnd();
        yield return new WaitForSeconds(1.3f);
        textBox.BeginStory("Hmmmmm... What's this whiny sound coming from the carriage?");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("No way!!!");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("To think that I would find the one thing that makes the bad guys piss their pants out of fear.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("The one thing that makes an over powered badass character menacing and serious.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("Just like every Bond villian has a white cat by their side, what I just found is...");
        yield return new WaitUntil(() => textBox.ready);
        //Enlarge
        textBox.LoadLine("A loli.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("Hohohoho! Now if I can just teach her to speak in riddles.");
        yield return new WaitUntil(() => textBox.ready);
        textBox.LoadLine("END THE STORY");
        yield return new WaitUntil(() => textBox.ready);

        EndCutscene();
    }

    private void EndCutscene()
    {
        //Party data
        PartyData pd = new PartyData().Load();
        int[] id = new int[pd.levels.Length + 1];
        int[] lv = new int[pd.levels.Length + 1];
        for (int i = 0; i < pd.levels.Length; i++)
        {
            id[i] = pd.ids[i];
            lv[i] = pd.levels[i];
        }
        id[pd.levels.Length] = 1;
        lv[pd.levels.Length] = 1;
        pd.levels = lv;
        pd.ids = id;
        pd.Save();

        SaveData sd = new SaveData().Load();

        Quest q = sm.FindQuest(sd.quest);

        sd.scene = 2;
        sd.Save();
        tm.LoadScene(sd.Load().scene);
    }
}