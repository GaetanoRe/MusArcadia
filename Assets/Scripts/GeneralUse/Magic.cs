using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.GeneralUse
{
    [CreateAssetMenu(fileName = "New Spell" ,menuName = "Magic")]
    public class Magic : ScriptableObject
    {
        public enum ElementType{
            Spark,
            Fire, 
            Ice,
            Light,
            Dark,
            Nature,
            Plague,
            Earth,
            Water
        }

        public bool defensive;

        public ElementType elementType;
        public new string name;
        public string description;
        public float maxDamage;
        public float minDamage;
        public float manaCost;
    }
}