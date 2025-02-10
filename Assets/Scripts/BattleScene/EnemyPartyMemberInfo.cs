using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
using UnityEditor.MPE;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    [CreateAssetMenu(fileName = "New Enemy Party Member", menuName = "Entity/Enemy Party Member")]
    public class EnemyPartyMemberInfo : Entity
    {
        public enum EnemyType
        {
            Brawler,
            Strategist,
            Healer,
            Boss
        }

        public EnemyType type;

        public Item itemHeld;

        public override void attack(Entity subject){
            if (subject is PlayerPartyMemberInfo){
                float damage = UnityEngine.Random.Range(meleeDamage / 2, meleeDamage);
                if(critChanceRoll()){
                    damage *= 2;
                    subject.status = StatusEffects.Dazed;
                }
                subject.takeDamage(damage);
            }
        }
        public override void castMagic(Entity subject, Magic spell){
            if(type is EnemyType.Strategist || type is EnemyType.Healer){
                if(!spell.defensive && subject is PlayerPartyMemberInfo){
                    base.castMagic(subject, spell);
                }
                else if (subject is EnemyPartyMemberInfo){
                    base.castMagic(subject, spell);
                }
            }
            
            else if (type == EnemyType.Brawler){
                throw new NotImplementedException();
            }

            mana -= spell.manaCost;
        }

        public override void useItem(Entity subject, Consumable item)
        {
            throw new NotImplementedException();
        }

        public override void run(){
            throw new NotImplementedException();
        }
    }
}