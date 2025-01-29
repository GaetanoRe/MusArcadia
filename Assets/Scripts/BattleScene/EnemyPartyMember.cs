using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    public class EnemyPartyMember : Entity
    {
        void Start(){

        }

        public override void attack(Entity subject){
            if (subject is PlayerPartyMember){
                subject.takeDamage(meleeDamage);
            }
        }
        public override void castMagic(Entity subject, Magic spell){
            
        }
        public override void run(){
            
        }
    }
}