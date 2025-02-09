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
     
        public Party playerParty; // The Player Party

        public Party enemyParty; // The Enemy Party


        public bool goesFirst; 

        private List<Entity> currentPartyTurn;
        private int enemyPartySize;


        private void Start()
        {
            state = BattleState.Start;
            SetupBattle();
        }


        void BattleLoop(){

        }

        void SetupBattle(){
            enemyPartySize = UnityEngine.Random.Range(0, 7);

            for(int i = 0; i < enemyPartySize; i++){
                enemyParty.partyInfo[i] = enemyPool[UnityEngine.Random.Range(0, enemyPool.Count)];
            }
            
           
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