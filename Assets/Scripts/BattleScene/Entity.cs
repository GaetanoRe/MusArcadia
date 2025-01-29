using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using System.Collections.Generic;
using TMPro;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    public abstract class Entity: MonoBehaviour{

        private float _health;
        private float _mana;
        public Stats statSheet;

        public List<Magic.ElementType> strongAgainst;
        public List<Magic.ElementType> weakAgainst;

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


        public float health {
            get => _health;
            set{
                if(value > maxHealth){
                    _health = maxHealth;
                }
                else if(value < 0){
                    _health = 0;
                }
                else{
                    _health = value;
                }
            }}
        public float mana{
            get => _mana;
            set{
                if(value > maxMana){
                    _mana = maxMana;
                }
                else if(value < 0){
                    _mana = 0;
                }
                else{
                    _mana = value;
                }
            }
        }

        public void Start(){

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