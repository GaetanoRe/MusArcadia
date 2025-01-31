using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
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

        public override void attack(Entity subject){
            /**if (subject is PlayerPartyMemberInfo){
                subject.takeDamage(meleeDamage);
            }**/
        }
        public override void castMagic(Entity subject, Magic spell){
            
        }
        public override void run(){
            
        }
    }
}