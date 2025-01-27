using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    public class PlayerPartyMember : Entity
    {

        private float _exp;
        public float exp{ get{
            return _exp;
        } 
        set{
            _exp = value;
            while(_exp >= expCap){
                _exp = value - expCap;
                LevelUp();
                
            }
        } }
        public float expCap {
            get{
                return statSheet.level * 3.05f + 100;
            }
        }

        void Start()
        {
            
        }

        public override void attack(Entity subject){
            
        }

        public override void castMagic(Entity subject){

        }

        public override void run()
        {
            
        }

        public virtual void LevelUp(){
            statSheet.level++;
            
        }
    }
}