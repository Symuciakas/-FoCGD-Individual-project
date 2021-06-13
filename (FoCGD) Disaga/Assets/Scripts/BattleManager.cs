using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public Animator battleEndAnimator;
    private AudioSource battleMusic;
    private TransitionManager tm;
    private SaveManager sm = new SaveManager();
    private BattleCharacter[] battleCharacters;
    private SaveData sd;
    private BattleData bd;
    private PartyData pd;
    private BattleCharacter target;

    public GameObject hitmark;

    public BarManager[] barManagers;

    private int[,] targetMatrix = { { 5, 2, 8, 11 },
                                    { 3, 0, 6, 9 },
                                    { 4, 1, 7, 10 } };
    private int targetI = 1;
    private int targetJ = 2;
    private int targetIOld;
    private int targetJOld;
    private bool hitmarkCanMove = false;
    private IEnumerator thcm;

    private BattleUIManager battleUiManager;

    private Button[] actionButtons = new Button[4];

    private bool actionSet;

    private Action action = new Action();

    private BattleCharacter bc;

    private bool battleOver = false;
    private bool victory = false;

    private bool canLoad = true;

    void Start()
    {
        tm = FindObjectOfType<TransitionManager>();
        sd = new SaveData().Load();
        bd = new BattleData().Load();
        pd = new PartyData().Load();
        battleMusic = GetComponent<AudioSource>();
        battleMusic.volume = sm.GetVolume();

        battleUiManager = FindObjectOfType<BattleUIManager>();

        this.gameObject.transform.GetChild(12).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Backgrounds/" + bd.background);

        for (int i = 0; i < pd.ids.Length; i++)
        {
            BattleCharacter bc = Instantiate(Resources.Load<BattleCharacter>("BattleCharacters/" + pd.ids[i].ToString()), this.gameObject.transform.GetChild(i).transform.position, transform.rotation);
            bc.name = pd.ids[i].ToString();
            bc.GetComponent<SpriteRenderer>().sortingLayerName = "Battle" + (i + 1).ToString();
            bc.level = pd.levels[i];
            bc.playable = true;
            bc.SetCharacter(sm.FindAlly(pd.ids[i]));
        }

        for (int i = 0; i < bd.enemyIds.Length; i++)
        {
            BattleCharacter bc = Instantiate(Resources.Load<BattleCharacter>("BattleCharacters/" + bd.enemyIds[i].ToString()), this.gameObject.transform.GetChild(i + 6).transform.position, transform.rotation);
            bc.name = bd.enemyIds[i].ToString();
            bc.GetComponent<SpriteRenderer>().sortingLayerName = "Battle" + (i + 1).ToString();
            bc.level = bd.enemyLvls[i];
            bc.playable = false;
            bc.SetCharacter(sm.FindEnemy(bd.enemyIds[i]));
        }

        battleCharacters = FindObjectsOfType<BattleCharacter>();

        barManagers = FindObjectsOfType<BarManager>();

        foreach (Button b in FindObjectsOfType<Button>())
        {
            if (b.name == "1")
            {
                actionButtons[0] = b;
            }
            else if (b.name == "2")
            {
                actionButtons[1] = b;
            }
            else if (b.name == "3")
            {
                actionButtons[2] = b;
            }
            else if (b.name == "4")
            {
                actionButtons[3] = b;
            }
        }

        UpdateTarget();
        if (thcm != null)
        {
            StopCoroutine(thcm);
        }
        hitmarkCanMove = false;

        tm.LoadEnd();

        StartCoroutine(Battle());
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && hitmarkCanMove)
        {
            hitmarkCanMove = false;
            if (CanBeTargeted(targetI, (targetJ + 1) % 4))
            {
                targetJ = (targetJ + 1) % 4;
            }
            UpdateTarget();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && hitmarkCanMove)
        {
            hitmarkCanMove = false;
            if (CanBeTargeted(targetI, (targetJ + 3) % 4))
            {
                targetJ = (targetJ + 3) % 4;
            }
            UpdateTarget();
        }
        if (Input.GetAxisRaw("Vertical") < 0 && hitmarkCanMove)
        {
            hitmarkCanMove = false;
            if (CanBeTargeted((targetI + 1) % 3, targetJ))
            {
                targetI = (targetI + 1) % 3;
            }
            UpdateTarget();
        }
        if (Input.GetAxisRaw("Vertical") > 0 && hitmarkCanMove)
        {
            hitmarkCanMove = false;
            if (CanBeTargeted((targetI + 2) % 3, targetJ))
            {
                targetI = (targetI + 2) % 3;
            }
            UpdateTarget();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            float f = sm.GetVolume();
            if (f < 1f)
            {
                f = f + 0.1f;
            }
            sm.SetVolume(f);
            battleMusic.volume = sm.GetVolume();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            float f = sm.GetVolume();
            if (f > 0f)
            {
                f = f - 0.1f;
            }
            sm.SetVolume(f);
            battleMusic.volume = sm.GetVolume();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && canLoad)
        {
            canLoad = false;
            tm.LoadScene(0);
        }
    }

    private void UpdateTarget()
    {
        hitmark.transform.position = this.gameObject.transform.GetChild(targetMatrix[targetI, targetJ]).transform.position;
        foreach (BattleCharacter bc in battleCharacters)
        {
            if (bc.transform.position == hitmark.transform.position)
            {
                target = bc;
                UpdateBars();
            }
        }
        thcm = ThawHitmarkCanMove(0.2f);
        StartCoroutine(thcm);
    }

    private void UpdateBars()
    {
        foreach (BarManager bm in barManagers)
        {
            if (bm.name == "LifeBar")
            {
                bm.SetSliderMaxValue(target.character.lifePoints * target.level);
                bm.SetSliderValue(target.lifePoints);
            }
            else if (bm.name == "PhysicalBar")
            {
                bm.SetSliderMaxValue(target.character.bodyHealth * target.level);
                bm.SetSliderValue(target.bodyHealth);
            }
            else if (bm.name == "MagicBar")
            {
                bm.SetSliderMaxValue(target.character.mindHealth * target.level);
                bm.SetSliderValue(target.mindHealth);
            }
            else if (bm.name == "SpiritBar")
            {
                bm.SetSliderMaxValue(target.character.spiritHealth * target.level);
                bm.SetSliderValue(target.spiritHealth);
            }
        }
    }

    private bool CanBeTargeted(int i, int j)
    {
        foreach (BattleCharacter b in battleCharacters)
        {
            if (b.transform.position == this.gameObject.transform.GetChild(targetMatrix[i, j]).transform.position)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator ThawHitmarkCanMove(float f)
    {
        yield return new WaitForSeconds(f);
        hitmarkCanMove = true;
    }

    IEnumerator Battle()
    {
        yield return new WaitForSeconds(2f);
        while (!battleOver)//Check battle over?
        {
            List<BattleCharacter> targets = new List<BattleCharacter>();
            bc = GetNextInLine();
            if (bc.playable)
            {
                actionSet = false;
                //Show hitmark
                hitmark.transform.position = this.gameObject.transform.GetChild(targetMatrix[targetI, targetJ]).transform.position;
                hitmarkCanMove = true;

                //enable battle ui manager
                battleUiManager.enabled = true;

                foreach (Button b in actionButtons)
                {
                    Action a = sm.FindAction(bc.GetAction(b.name));
                    if (a != null)
                    {
                        b.GetComponentInChildren<Text>().text = a.name;
                        b.interactable = true;
                    }
                    else
                    {
                        b.GetComponentInChildren<Text>().text = "-";
                    }
                }
                yield return new WaitUntil(() => actionSet);
                foreach (Button b in actionButtons)
                {
                    b.GetComponentInChildren<Text>().text = "-";
                    b.interactable = false;
                }

                //Disable UI
                battleUiManager.enabled = false;

                if (thcm != null)
                {
                    StopCoroutine(thcm);
                }
                hitmarkCanMove = false;

                targetIOld = targetI;
                targetJOld = targetJ;
            }
            else
            {
                targetIOld = targetI;
                targetJOld = targetJ;

                Action a2 = sm.FindAction(bc.GetAction("2"));
                Action a3 = sm.FindAction(bc.GetAction("3"));
                Action a4 = sm.FindAction(bc.GetAction("4"));

                int f = (int)Mathf.Ceil(Random.Range(0f, 4f));
                if (a4 != null && f > 3)
                {
                    action = a4;
                }
                else if (a3 != null && f > 2)
                {
                    action = a3;
                }
                if (a2 != null && f > 1)
                {
                    action = a2;
                }
                else
                {
                    action = sm.FindAction(bc.GetAction("1"));
                }
                int count = 0;
                foreach (BattleCharacter b in battleCharacters)
                {
                    if (b.playable == action.harmful)
                    {
                        count++;
                    }
                }

                int t = (int)Mathf.Ceil(Random.Range(0f, count));
                if (t == 0) t = 1;
                if (action.harmful)
                {
                    FindTargetInMatrix(0, 2, t);
                }
                else
                {
                    FindTargetInMatrix(2, 4, t);
                }

                //Shows Hitmark
                UpdateTarget();
                //Might be bad
                if (thcm != null)
                {
                    StopCoroutine(thcm);
                }
                hitmarkCanMove = false;
            }

            //Hide hitmark
            hitmark.transform.position = new Vector2(-10, -10);

            //Get all targets
            if (action.aoe)
            {
                foreach (BattleCharacter b in battleCharacters)
                {
                    if (b.playable == target.playable)
                    {
                        targets.Add(b);
                    }
                }
            }
            else
            {
                targets.Add(target);
            }

            //Execute action
            bc.Action(action, targets);

            UpdateBars();

            yield return new WaitForSeconds(1f);

            battleCharacters = FindObjectsOfType<BattleCharacter>();

            bool hasP = false;
            bool hasU = false;
            foreach (BattleCharacter b in battleCharacters)
            {
                if (b.playable)
                {
                    hasP = true;
                }
                else
                {
                    hasU = true;
                }
            }

            if (!hasP)
            {
                victory = false;
                battleOver = true;
            }
            if (!hasU)
            {
                victory = true;
                battleOver = true;
            }

            targetI = targetIOld;
            targetJ = targetJOld;

            UpdateTarget();
            //Might be bad
            if (thcm != null)
            {
                StopCoroutine(thcm);
            }
            hitmarkCanMove = false;
        }

        //Hide hitmark
        hitmark.transform.position = new Vector2(-10, -10);

        if (victory)
        {
            battleEndAnimator.SetTrigger("Victory");

            yield return new WaitForSeconds(2f);

            //Cutscene not implemented

            Quest q = sm.FindQuest(sd.quest);

            int c = 0;
            int[] en = new int[q.enemies.Length - 1];
            for (int i = 0; i < q.enemies.Length; i++)
            {
                if (q.enemies[i] != bd.enemyChibiId)
                {
                    en[c] = q.enemies[i];
                    c++;
                }
            }
            q.enemies = en;
            //print(q.enemies.Length);
            sm.SetQuest(q);

            if (sm.FindHostileNPC(bd.enemyChibiId).cutsceneAfter != "null")
            {
                sd.cutscene = sm.FindHostileNPC(bd.enemyChibiId).cutsceneAfter;
                sd.scene = 1;
                sd.Save();
                tm.LoadScene(sd.Load().scene);
            }
            else
            {
                sd.scene = 2;
                sd.Save();
                tm.LoadScene(sd.Load().scene);
            }
        }
        else
        {
            battleEndAnimator.SetTrigger("Defeat");

            print("NOT IMPLEMENTED!");
            print("Player defeated");
        }
    }

    public void ActionButton1Click()
    {
        action = sm.FindAction(bc.GetAction("1"));
        actionSet = true;
    }

    public void ActionButton2Click()
    {
        action = sm.FindAction(bc.GetAction("2"));
        actionSet = true;
    }

    public void ActionButton3Click()
    {
        action = sm.FindAction(bc.GetAction("3"));
        actionSet = true;
    }

    public void ActionButton4Click()
    {
        action = sm.FindAction(bc.GetAction("4"));
        actionSet = true;
    }

    private BattleCharacter GetNextInLine()
    {
        int nextAttackTime = 10000000;
        BattleCharacter bc = null;
        foreach (BattleCharacter battleCharacter in battleCharacters)
        {
            if (battleCharacter.nextAttack < nextAttackTime)
            {
                bc = battleCharacter;
                nextAttackTime = battleCharacter.nextAttack;
            }
        }
        foreach (BattleCharacter battleCharacter in battleCharacters)
        {
            battleCharacter.nextAttack = battleCharacter.nextAttack - nextAttackTime;
            battleCharacter.nextNextAttack = battleCharacter.nextNextAttack - nextAttackTime;
        }
        bc.nextAttack = bc.nextNextAttack;
        bc.nextNextAttack = bc.nextNextAttack + 1000000 / bc.character.speed;
        return bc;
    }

    private void FindTargetInMatrix(int n, int m, int t)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = n; j < m; j++)
            {
                if (targetMatrix[i, j] == t - 1)
                {
                    targetI = i;
                    targetJ = j;
                }
            }
        }
    }
}