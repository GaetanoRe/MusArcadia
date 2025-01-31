using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
using UnityEngine;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    [CreateAssetMenu(fileName = "New Player Party Member", menuName = "Entity/Player Party Member")]
    public class PlayerPartyMemberInfo : Entity
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
            /**if(subject is EnemyPartyMember){
                subject.takeDamage(meleeDamage);
            }**/
        }

        public override void castMagic(Entity subject, Magic spell){

        }

        public override void run()
        {
            
        }

        public virtual void LevelUp(){
            statSheet.level++;
            
        }
    }
}