using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    public class PlayerClass : Entity
    {

        private float _exp;
        public Inventory inventory;
        public float exp{ get{
            return _exp;
        } 
        set{
            if(value >= expCap){
                _exp = value - expCap;
                statSheet.level++;
            }
        } }
        public float expCap {
            get{
                return statSheet.level * 3.05f + 100;
            }
        }

        public PlayerClass() : base(){
            exp = 0;
        }

        public PlayerClass(int level, int consti, int dex, int str, int intel, int agil) : base(level, consti, dex, str, intel, agil) {
            exp = 0;
        }


        public override void attack(Entity subject){
            
        }

        public override void castMagic(Entity subject){

        }
    }
}