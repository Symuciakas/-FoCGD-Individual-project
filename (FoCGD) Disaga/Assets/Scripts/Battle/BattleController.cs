using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Battle b = new Battle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
/*
class Battle
{
    List<Character> player;
    List<Character> enemies;
    public Battle(List<Character> characters, string filePath)
    {
        player = characters;
        enemies = populateEnemies(filePath);
        //Open battle scene with stuff
    }
    
    private List<Character> populateEnemies(string s)
    {
        List<Character> c = new List<Character>();

        //Open file
        //Generate characters

        return c;
    }
}

class Character
{
    int id;
    string name;
    int lvl;
    int pos;
    int turn;

    int ph;
    int sh;
    int mh;

    int pa;
    int sa;
    int ma;

    int spd;

    //sprites, animations

    List<Skill> skills;

    List<Buff> buffs;
    public Character()
    {

    }

    public void takeDamage(int damage, int damageType, int element)
    {
        switch(damageType)
        {
            case 0:
                ph = ph - damage;
                break;
            case 1:
                sh = sh - damage;
                break;
            case 2:
                mh = mh - damage;
                break;
        }
    }

    public void buff(Buff buff)
    {
        ph = ph + buff.phA;
        sh = sh + buff.shA;
        mh = mh + buff.mhA;

        pa = pa + buff.paA;
        sa = sa + buff.saA;
        ma = ma + buff.maA;

        buffs.Add(buff);
    }

    void buffEnd(Buff buff)
    {
        ph = ph + buff.phA;
        sh = sh + buff.shA;
        mh = mh + buff.mhA;

        pa = pa + buff.paA;
        sa = sa + buff.saA;
        ma = ma + buff.maA;

        buffs.Remove(buff);
    }
}

abstract class Skill
{
    public int id;
    public string name;
    public Skill(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public abstract void activate(List<Character> targets);
}

class Attack : Skill
{
    int damage;
    int damageType;
    //int cost
    //int costType;
    //int element;
    Attack(int id, string name, int damage, int damageType) : base(id, name)
    {
        this.id = id;
        this.name = name;

        this.damage = damage;
        this.damageType = damageType;
    }
    public override void activate(List<Character> targets)
    {
        foreach(Character c in targets)
        {
            c.takeDamage(damage, damageType, element);
        }
    }
}

class Buff : Skill
{
    public int turns;

    public int phA;
    public int shA;
    public int mhA;

    public int paA;
    public int saA;
    public int maA;
    Buff(int id, string name, int turns, int phA, int shA, int mhA, int paA, int saA, int maA) : base(id, name)
    {
        this.id = id;
        this.name = name;

        this.turns = turns;

        this.phA = phA;
        this.shA = shA;
        this.mhA = mhA;

        this.paA = paA;
        this.saA = saA;
        this.maA = maA;
    }
    public override void activate(List<Character> targets)
    {
        foreach(Character c in targets)
        {
            c.buff(this);
        }
    }
}
*/
