using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Character
{
    public int id;
    public string charcterName;

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

    public int action1;
    public int action2;
    public int action3;
    public int action4;

    public int speed;

    public string[] traits;

    public string[] description;

    public Character()
    {
        id = 1001;
        charcterName = "Stupid grunt";
        lifePoints = 10;
        bodyHealth = 15;
        mindHealth = 1;
        spiritHealth = 2;

        physicalDefense = 0;
        magicResistance = 0;
        mentalFortitude = 0;
        spiritPower = 0;

        physicalAttack = 1;
        magicAttack = 0;
        mentalAttack = 0;
        spiritAttack = 0;

        physicalCritRate = 0;
        magicalCritRate = 0;
        mentalCritRate = 0;
        spiritCritRate = 0;

        action1 = 0;
        action2 = -1;
        action3 = -1;
        action4 = -1;

        speed = 500;

        traits = new string[] { "Idiot", "Weak" };

        description = new string[] { "Just some grunt" };
    }

    /*public void SaveAsEnemy()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/CharacterData/Enemies.json", JsonUtility.ToJson(this));
    }

    public void SaveAsAlly()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/CharacterData/Allies.json", JsonUtility.ToJson(this));
    }*/
}