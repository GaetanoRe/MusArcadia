using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{

    [CreateAssetMenu(fileName = "Stats", menuName = "Stats")]
    public class Stats : ScriptableObject 
    {
        public int level;
        public int constitution;
        public int dexterity;
        public int strength;
        public int intelligence;
        public int agility;
       


        /**
            Method Name: Stats Default Constructor
            Description: Instantiates a level 1 StatSheet with default affinities
        **/
        public Stats()
        {
            level = 1;
            constitution = 10;
            dexterity = 10;
            strength = 10;
            intelligence = 10;
            agility = 10;
        }
        
        /**
            Method Name: Stats Parameterized Constructor
            Description: Instantiates an object for the stats that will be used by most Entities
        **/
        public Stats(int level, int consti, int dex, int str, int intel, int agil){
            this.level = level;
            constitution = consti;
            dexterity = dex;
            strength = str;
            intelligence = intel;
            agility = agil;

        }

        // Depreciated method vvv
        /**
            Method Name: ModifyStat
            Description: Modifies a stat with a given word representing the stat and a given value
        **/
       /*  public void ModifyStat(string statName, int value)
        {
            switch (statName.ToLower())
            {
                case "constitution":
                    constitution += value;
                    break;
                case "dexterity":
                    dexterity += value;
                    break;
                case "strength":
                    strength += value;
                    break;
                case "intelligence":
                    intelligence += value;
                    break;
                case "agility":
                    agility += value;
                    break;
                default:
                    throw new ArgumentException($"Stat {statName} not found.");
            }
        }*/
    }
}