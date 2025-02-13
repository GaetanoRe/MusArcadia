using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using System.Collections.Generic;
using TMPro;
namespace MusArcadia.Assets.Scripts.BattleScene
{

    public enum HitStatus{
        CriticalHit,
        Hit,
        Miss
    }
    public abstract class Entity: ScriptableObject{

        public enum StatusEffects
        {
            None,
            Burned,
            Plague,
            Frozen,
            Paralysis,
            Dazed
        }

        public enum StatBias{
            None,
            Constitution,
            Dexterity,
            Strength,
            Intelligence,
            Agility


        }


        private float _health;
        private float _mana;
        public Stats statSheet;
        public Sprite entitySprite;
        public Sprite entityModel;

        public List<Magic.ElementType> strongAgainst;
        public List<Magic.ElementType> weakAgainst;
        public List<Magic.ElementType> nullAgainst;

        public bool missed;

        public StatBias statBias;

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
                return statSheet.strength * 0.4005f;
            }
        }

        public float physicalDefense {
            get{
                return statSheet.strength * 0.40005f + statSheet.constitution * 0.25426f;
            }
        }

        public float magicalDefense{
            get{
                return statSheet.intelligence * 0.80005f + statSheet.constitution * 0.15426f;
            }
        }

        public float accuracy{
            get{
                return statSheet.dexterity * 0.522245f;
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
            reevalStats();

            Debug.Log($"{this.name} initialized - Health: {_health}/{maxHealth}, Mana: {_mana}/{maxMana}");


        }


        public virtual void takeDamage(float amount){
            health -= amount;
            Debug.Log($"{this} took {amount} damage and now has {health}/{maxHealth} health.");
        }

        public virtual bool critChanceRoll(){
            float critChanceRoll = UnityEngine.Random.Range(0, 20);
            return critChanceRoll > 17;
        }

        public virtual void reevalStats(){
            for(int i = 0; i < statSheet.level; i++){
                switch(statBias){
                case StatBias.Constitution:
                    statSheet.constitution++;;
                    break;
                case StatBias.Dexterity:
                    statSheet.dexterity++; 
                    break;
                case StatBias.Strength:
                    statSheet.strength++;
                    break;
                case StatBias.Intelligence:
                    statSheet.intelligence++;
                    break;
                case StatBias.Agility:
                    statSheet.agility++;
                    break;
                
                }
                statSheet.strength++;
                statSheet.intelligence++;
                statSheet.dexterity++;
                statSheet.agility++;
            }
            
        }

        public abstract HitStatus attack(Entity subject);
        public virtual void castMagic(Entity subject, Magic spell){
            if(spell.defensive){
                subject.takeDamage(UnityEngine.Random.Range(spell.minDamage * (statSheet.intelligence * 0.25f), spell.maxDamage * (statSheet.intelligence * 0.25f)) * -1f);
            }
            else{
                if(spell.statusEffects != StatusEffects.None){
                    float rollStatus = UnityEngine.Random.Range(0, 20);
                    if(rollStatus > 10){
                        subject.status = spell.statusEffects;
                    }
                }
                float rollAccuracy = UnityEngine.Random.Range(0, 20) + accuracy;
                if(rollAccuracy > 10 && !spell.defensive){
                    subject.takeDamage(UnityEngine.Random.Range(spell.minDamage * (statSheet.intelligence * 0.25f), spell.maxDamage * (statSheet.intelligence * 0.25f)));
                }
            }
            
        }

        public abstract void useItem(Entity subject, Consumable item);
        public abstract void run();

    }
}