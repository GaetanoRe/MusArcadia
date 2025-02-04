using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using System.Collections.Generic;
using TMPro;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    public abstract class Entity: ScriptableObject{

        public enum StatusEffects
        {
            None,
            Burned,
            Plague,
            Frozen,
            Paralysis
        }


        private float _health;
        private float _mana;
        public Stats statSheet;
        public Sprite enitiySprite;

        public List<Magic.ElementType> strongAgainst;
        public List<Magic.ElementType> weakAgainst;
        public List<Magic.ElementType> nullAgainst;

        public StatusEffects status;


        public List<Magic> spellBook;

        public float maxHealth{
            get{
                return (statSheet.constitution * 5.5f) + (float)(statSheet.level * 2.25);
            }
        }
        public float maxMana{
            get{
                return (statSheet.intelligence * 2.5f) + (float)(statSheet.level * 1.25);
            }
        }

        public float meleeDamage {
            get{
                return statSheet.strength * 3.005f;
            }
        }

        public float physicalDefense {
            get{
                return statSheet.strength * 1.20005f + statSheet.constitution * 1.35426f;
            }
        }

        public float magicalDefense{
            get{
                return statSheet.intelligence * 1.20005f + statSheet.constitution * 1.35426f;
            }
        }


        public float health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                if (_health > maxHealth)
                {
                    _health = maxHealth;
                }
                if (_health < 0)
                {
                    _health = 0;
                }
            }

        }
        public float mana
        {
            get
            {
                return _mana;
            }
            set
            {
                _mana = value;
                if (_mana > maxMana)
                {
                    _mana = maxMana;
                }
                if (_mana < 0)
                {
                    _mana = 0;
                }
            }
        }

        public void Initialize()
        {
            if (statSheet == null)
            {
                Debug.LogError($"{this.name} has no statSheet assigned!");
                return;
            }

            health = maxHealth;
            mana = maxMana;

            Debug.Log($"{this.name} initialized - Health: {_health}/{maxHealth}, Mana: {_mana}/{maxMana}");
        }


        public virtual void takeDamage(float amount){
            health -= amount;
            Debug.Log($"{this} took {amount} damage and now has {health}/{maxHealth} health.");
        }

        public abstract void attack(Entity subject);
        public abstract void castMagic(Entity subject, Magic spell);
        public abstract void run();

    }
}