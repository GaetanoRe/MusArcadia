using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.UI;
using MusArcadia.Assets.Scripts.BattleScene;
using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;

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

        public GameObject Selector;

        public List<GameObject> enemyPool; // The Pool of Possible Enemies in the Area.
     
        public Party playerParty; // The Player Party

        public Party enemyParty; // The Enemy Party

        public Action currentAction;

        int selected = 0;

        public bool goesFirst; 

        public Party currentPartyTurn;
        public Party currentOppTeam;
        private Unit currentTurn;
        private Unit currentTarget;
        private int enemyPartySize;


        private void Start()
        {
            
            state = BattleState.Start;
            SetupBattle();
        }

        private void Update()
        {
            currentAction = battleUI.action;
            if (state != BattleState.Idle)
            {
                switch (currentAction)
                {
                    case Action.Fight:
                        Selector.SetActive(true);

                        // Ensure selected stays in bounds
                        if (selected >= enemyParty.party.Count)
                        {
                            selected = 0;
                        }

                        currentTarget = enemyParty.party[selected];

                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            selected++;
                            if (selected >= enemyParty.party.Count)
                            {
                                selected = 0; // Wrap around to first enemy
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            selected--;
                            if (selected < 0)
                            {
                                selected = enemyParty.party.Count - 1; // Wrap around to last enemy
                            }
                        }

                        // Check if selected is within bounds before using it
                        if (selected >= 0 && selected < enemyParty.partyPos.Count)
                        {
                            Selector.transform.position = enemyParty.partyPos[selected].position + new Vector3(0, 2, 0);
                        }

                        break;
                }
                TakeAction(currentTurn.playerInfo, currentTarget.playerInfo, currentAction, null, null);
            }



        }
    void BattleLoop(){

        }

        void SetupBattle(){
            
            SetupEnemyParty();
           
        }

        void StartTurn(){
            state = BattleState.Idle;

            
        }

        void TakeAction(Entity user, Entity target, Action action, Magic spell, Consumable item){
            switch(action){
                case Action.Fight:
                    user.attack(target);
                    break;
                case Action.Magic:
                    user.castMagic(target, spell);
                    break;
                case Action.Item:
                    user.useItem(target, item);
                    break;
                case Action.Run:
                    Application.Quit();
                    break;
            }
        }

        void SetupEnemyParty()
        {

            int partySize = UnityEngine.Random.Range(1, 4); // Random number of enemies (e.g., 1 to 3)

            for (int i = 0; i < partySize; i++)
            {
                // Pick a random enemy prefab
                GameObject enemyPrefab = enemyPool[UnityEngine.Random.Range(0, enemyPool.Count)];
                GameObject enemyGO = Instantiate(enemyPrefab, enemyParty.transform);

                // Get the `Unit` component and add to `enemyParty`
                Unit enemyUnit = enemyGO.GetComponent<Unit>();
                if (enemyUnit != null)
                {
                    enemyParty.party.Add(enemyUnit);
                    enemyUnit.transform.position = enemyParty.partyPos[i].position;
                }
            }

            enemyParty.Initialize();
        }

    }

}