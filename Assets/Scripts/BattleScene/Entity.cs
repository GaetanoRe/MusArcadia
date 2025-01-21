using UnityEngine;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    public abstract class Entity{

        private float _health;
        private float _mana;
        public Stats statSheet;

        public float maxHealth{
            get{
                return statSheet.constitution * 5.5f;
            }
        }
        public float maxMana{
            get{
                return statSheet.intelligence * 2.5f;
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

        public Entity(){
            statSheet = new Stats();
            health = maxHealth;
            mana = maxMana;
        }

        public Entity(int level, int consti, int dex, int str, int intel, int agil){
            statSheet = new Stats(level, consti, dex, str, intel, agil);
            health = maxHealth;
            mana = maxMana;
        }

        public virtual void takeDamage(int amount){
            health -= amount;
            Debug.Log($"{this} took {amount} damage and now has {health}/{maxHealth} health.");
        }

        public abstract void attack(Entity subject);
        public abstract void castMagic(Entity subject);

    }
}