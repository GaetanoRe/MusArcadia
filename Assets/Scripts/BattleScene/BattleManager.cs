using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.UI;
using MusArcadia.Assets.Scripts.BattleScene;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    public enum BattleState{
        Start,
        Idle,
        Action,
        Execute,
        End
    }

    public enum Action{
        Fight,
        Item,
        Magic,
        Run
    }

    public class BattleManager : MonoBehaviour
    {
        public BattleState state;
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

        public BattleUI battleUI;
        public EffectsManager effectsManager;

        public List<EnemyPartyMemberInfo> enemyPool; // The Pool of Possible Enemies in the Area.
     
        public List<PlayerPartyMemberInfo> playerParty; // The Player Party

        public bool goesFirst; 

        private List<Entity> currentPartyTurn;
        private List<EnemyPartyMemberInfo> enemyParty;


        private void Start()
        {
            state = BattleState.Start;
            SetupBattle();
        }


        void BattleLoop(){

        }

        void SetupBattle(){
            
        }

        void StartTurn(){
            state = BattleState.Action;
            
        }

        void TakeAction(Entity user, Entity target){

        }

        private void SetupEnemyParty(){
            

        }

    }

}