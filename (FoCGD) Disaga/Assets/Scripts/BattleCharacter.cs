using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public Animator animator;
    public Animator damageAnimator;

    public Character character;
    public int level;
    public int nextAttack;
    public int nextNextAttack;
    public bool playable;

    //Stats
    public int lifePoints;
    public int bodyHealth;
    public int mindHealth;
    public int spiritHealth;

    public int physicalDefense;
    public int magicResistance;
    public int mentalFortitude;
    public int spiritPower;

    public int physicalAttack;
    public int magicAttack;
    public int mentalAttack;
    public int spiritAttack;

    public int physicalCritRate;
    public int magicalCritRate;
    public int mentalCritRate;
    public int spiritCritRate;

    public int speed;

    public bool active = true;

    private void Start()
    {

    }

    public void Action(Action action, List<BattleCharacter> targets)
    {
        float r = 0, g = 0, b = 0, a = 0;
        int n = targets.Count;
        animator.SetTrigger("Attack");
        StartCoroutine(delayedTrigger("Idle", 0.3333333f));
        bool crit = false;
        float f = Random.Range(0f, 100f);
        int damage = 0;
        int[] defence = new int[n];
        switch (action.type)
        {
            case 1:
                crit = f <= physicalCritRate;
                damage = (int)(physicalAttack * level * action.mult);
                for (int i = 0; i < defence.Length; i++)
                {
                    defence[i] = (targets[i].physicalDefense * targets[i].level);
                }
                r = 0.5f;
                g = 0.5f;
                b = 0.5f;
                a = 1f;
                break;
            case 2:
                crit = f <= magicalCritRate;
                damage = (int)(magicAttack * level * action.mult);
                for (int i = 0; i < defence.Length; i++)
                {
                    defence[i] = (targets[i].magicResistance * targets[i].level);
                }
                r = 0.2f;
                g = 0.2f;
                b = 0.7f;
                a = 0.8f;
                break;
            case 3:
                crit = f <= mentalCritRate;
                damage = (int)(mentalAttack * level * action.mult);
                for (int i = 0; i < defence.Length; i++)
                {
                    defence[i] = (targets[i].mentalFortitude * targets[i].level);
                }
                r = 0.9f;
                g = 0.7f;
                b = 0.7f;
                a = 0.8f;
                break;
            case 4:
                crit = f <= spiritCritRate;
                damage = (int)(spiritAttack * level * action.mult);
                for (int i = 0; i < defence.Length; i++)
                {
                    defence[i] = (targets[i].spiritPower * targets[i].level);
                }
                r = 0.7f;
                g = 0.7f;
                b = 0.2f;
                a = 0.8f;
                break;
        }
        if (action.harmful)
        {
            for (int i = 0; i < n; i++)
            {
                targets[i].TakeDamage(action.id, damage, defence[i], crit, action.target, r, g, b, a);
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                targets[i].HealDamage(action.id, damage, crit, action.target);
            }
        }

    }

    private void TakeDamage(int id, int damage, int defence, bool crit, int target, float r, float g, float b, float a)
    {
        if (crit)
        {
            damage = damage * 2;
            r = r * r;
            g = g * g;
            b = b * b;
            a = Mathf.Sqrt(a);
        }
        if (damage - defence > 0)
        {
            switch (target)
            {
                case 1:
                    if (damage - defence > 0)
                    {
                        lifePoints = lifePoints - (damage - defence);
                        if (crit)
                        {
                            StartCoroutine(DelayedParticles(0.2f, "BloodSplatterA"));
                        }
                        else
                        {
                            StartCoroutine(DelayedParticles(0.2f, "BloodSplatterV"));
                        }
                    }
                    break;
                case 2:
                    if (damage - defence > 0)
                    {
                        bodyHealth = bodyHealth - (damage - defence);
                        if (crit)
                        {
                            StartCoroutine(DelayedParticles(0.2f, "BloodSplatterA"));
                        }
                        else
                        {
                            StartCoroutine(DelayedParticles(0.2f, "BloodSplatterV"));
                        }
                    }
                    print(2);
                    break;
                case 3:
                    if (damage - defence > 0)
                    {
                        mindHealth = mindHealth - (damage - defence);
                    }
                    break;
                case 4:
                    if (damage - defence > 0)
                    {
                        spiritHealth = spiritHealth - (damage - defence);
                    }
                    break;
            }
            StartCoroutine(DelayedDamagePopup(0.2f, damage - defence, r, g, b, a));
        }
        else
        {
            StartCoroutine(DelayedDamagePopup(0.2f, 0, r, g, b, a));
        }

        StartCoroutine(DelayedActionEffect(0.2f, id));

        if (character.lifePoints != 0 && lifePoints <= 0 || character.bodyHealth != 0 && bodyHealth <= 0 || character.mindHealth != 0 && mindHealth <= 0 || character.spiritHealth != 0 && spiritHealth <= 0)
        {
            active = false;
            StartCoroutine(Die());
        }
    }


    private void HealDamage(int id, int damage, bool crit, int target)
    {
        //int damageColor;
        if (crit)
        {
            damage = damage * 2;
        }
        switch (target)
        {
            case 1:
                lifePoints = lifePoints + damage;
                //damageColor = 1;
                break;
            case 2:
                bodyHealth = bodyHealth + damage;
                //damageColor = 2;
                break;
            case 3:
                mindHealth = mindHealth + damage;
                //damageColor = 3;
                break;
            case 4:
                spiritHealth = spiritHealth + damage;
                //damageColor = 4;
                break;
        }
        if (crit)
        {
            //Adjust color
        }
        //Display stuff
    }

    public void SetCharacter(Character c)
    {
        this.character = c;
        nextAttack = 1000000 / c.speed;
        nextNextAttack = nextAttack * 2;

        lifePoints = c.lifePoints * level;
        bodyHealth = c.bodyHealth * level;
        mindHealth = c.mindHealth * level;
        spiritHealth = c.spiritHealth * level;

        physicalDefense = c.physicalDefense * level;
        magicResistance = c.magicResistance * level;
        mentalFortitude = c.mentalFortitude * level;
        spiritPower = c.spiritPower * level;

        physicalAttack = c.physicalAttack * level;
        magicAttack = c.magicAttack * level;
        mentalAttack = c.mentalAttack * level;
        spiritAttack = c.spiritAttack * level;

        physicalCritRate = c.physicalCritRate * level;
        magicalCritRate = c.magicalCritRate * level;
        mentalCritRate = c.mentalCritRate * level;
        spiritCritRate = c.spiritCritRate * level;

        speed = (int)(c.speed * (1f + level / 10));
    }

    public int GetAction(string s)
    {
        if (s == "1")
        {
            return character.action1;
        }
        else if (s == "2")
        {
            return character.action2;
        }
        else if (s == "3")
        {
            return character.action3;
        }
        else if (s == "4")
        {
            return character.action4;
        }
        else
        {
            return -1;
        }
    }

    IEnumerator DelayedParticles(float f, string s)
    {
        yield return new WaitForSeconds(f);
        GameObject blood = Instantiate(Resources.Load<GameObject>("Particles/" + s), this.gameObject.transform.position, transform.rotation);
        Destroy(blood, 0.3f);
    }

    IEnumerator DelayedDamagePopup(float f, int damage, float r, float g, float b, float a)
    {
        yield return new WaitForSeconds(f);
        DamagePopup damagePopup = Instantiate(Resources.Load<DamagePopup>("UI/DamagePopup"), this.gameObject.transform.position, transform.rotation);
        damagePopup.Setup(damage, r, g, b, a);
    }

    IEnumerator DelayedActionEffect(float f, int i)
    {
        yield return new WaitForSeconds(f);
        damageAnimator.SetTrigger(i.ToString());
        yield return new WaitForSeconds(0.1666666666f);
        damageAnimator.SetTrigger("-1");
    }

    IEnumerator delayedTrigger(string s, float f)
    {
        yield return new WaitForSeconds(f);
        animator.SetTrigger(s);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}