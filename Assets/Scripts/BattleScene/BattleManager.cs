using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.UI;
using MusArcadia.Assets.Scripts.BattleScene;
using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using System.Collections;
using Unity.VisualScripting;
using JetBrains.Annotations;

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
        None,
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
        private int turn;
        private int enemyPartySize;


        void Start()
        {
            Selector.SetActive(false);
            playerParty.name = "PlayerParty";
            enemyParty.name = "EnemyParty";
            state = BattleState.Start;
            Debug.Log($"Setup Battle...");
            StartCoroutine(SetupBattle());
            currentAction = Action.None;
        }

        IEnumerator SetupBattle()
        {
            SetupEnemyParty();
            playerParty.Initialize();

            if (goesFirst)
            {
                currentPartyTurn = playerParty;
                currentOppTeam = enemyParty;
            }
            else
            {
                currentPartyTurn = enemyParty;
                currentOppTeam = playerParty;
            }
            Debug.Log($"Starting turn with {currentPartyTurn.name} going now");
            yield return StartTurn();
        }

        IEnumerator StartTurn()
        {
            state = BattleState.Idle;
            turn = 0;
            if(currentPartyTurn.Equals(playerParty))
            {
                battleUI.hidePlayerUI(false);
                state = BattleState.Action;
                currentAction = Action.None;
                Debug.Log($"Player turn now...");
                yield return PlayerAction();
            }
            else
            {
                battleUI.hidePlayerUI(true);
                Debug.Log($"Enemy Turn now...");
                yield return EnemyAction();
            }

            yield return null;
        }

        IEnumerator PlayerAction()
        {
            battleUI.currentTurn = (PlayerPartyMemberInfo) playerParty.partyInfo[turn];
            battleUI.UpdateUI();
            if (state != BattleState.Action) yield break;

            while (currentAction == Action.None) // Wait for an action to be selected
            {
                currentAction = battleUI.action;
                yield return null;
            }

            switch (currentAction)
            {
                case Action.Fight:
                    Debug.Log($"Fight Selected");
                    yield return SelectTarget(); // Call SelectTarget here!
                    break;

                case Action.Run:
                    Debug.Log("Player chose to run.");
                    state = BattleState.End; // Consider handling run logic here
                    break;
            }

            currentAction = Action.None; // Reset for the next turn
            if(fullTurns == 0 && halfTurns == 0){
                yield return SwitchTeamTurn();
            }

        }


        IEnumerator EnemyAction()
        {
            
            yield return new WaitForSeconds(2f);
        }


        IEnumerator SelectTarget()
        {
            Debug.Log($"Now Selecting Target...");
            if (enemyParty.party.Count == 0)
            {
                Debug.LogError("No enemies available!");
                yield break;
            }

            int selector = 0;
            bool selected = false;

            Selector.SetActive(true);
            Selector.transform.position = enemyParty.party[selector].transform.position + new Vector3(0, 2, 0);

            while (!selected)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) 
                {
                    selector = (selector - 1 + enemyParty.party.Count) % enemyParty.party.Count; // Wraps around
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow)) 
                {
                    selector = (selector + 1) % enemyParty.party.Count; // Wraps around
                }

                // Update Selector Position
                Selector.transform.position = enemyParty.party[selector].transform.position + new Vector3(0, 2, 0);

                if (Input.GetKeyDown(KeyCode.Space)) // Confirm Selection
                {
                    currentTarget = enemyParty.party[selector];
                    Debug.Log($"You selected {currentTarget.playerInfo.name}");
                    selected = true;
                    Selector.SetActive(false);
                    TakeAction(playerParty.partyInfo[turn],enemyParty.partyInfo[selector], null, null);
                }
            }
             yield return null; // Wait for next frame
        }



        IEnumerator SwitchTeamTurn(){
            if(currentPartyTurn.Equals(playerParty)){
                currentPartyTurn = enemyParty;
                currentOppTeam = playerParty;
            }
            else{
                currentPartyTurn = playerParty;
                currentOppTeam = enemyParty;
            }
            yield return StartTurn();
        }

        


        void TakeAction(Entity user, Entity target, Magic spell, Consumable item){
            Debug.Log($"{user.name} is executing {currentAction} on {target.name}!");
            switch(currentAction){
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
                Debug.Log("Exiting game.");
                Application.Quit();
                break;
            }
       }

        void SetupEnemyParty()
        {
            if (enemyParty == null)
            {
                Debug.LogError("enemyParty is NULL! Make sure it is assigned in the Unity Inspector.");
                return;
            }

            if (enemyParty.partyPos == null || enemyParty.partyPos.Count == 0)
            {
                Debug.LogError("enemyParty.partyPos is NULL or EMPTY! Make sure the enemy positions are set.");
                return;
            }

            if (enemyPool == null || enemyPool.Count == 0)
            {
                Debug.LogError("enemyPool is NULL or EMPTY! Add enemy prefabs to the enemy pool.");
                return;
            }

            enemyPartySize = UnityEngine.Random.Range(1, 4); // Random number of enemies (e.g., 1 to 3)

            for (int i = 0; i < enemyPartySize; i++)
            {
                if (i >= enemyParty.partyPos.Count) // Prevent out-of-bounds error
                {
                    Debug.LogWarning($"Not enough positions in enemyParty.partyPos! Skipping enemy {i}.");
                    break;
                }

                // Pick a random enemy prefab
                GameObject enemyPrefab = enemyPool[UnityEngine.Random.Range(0, enemyPool.Count)];
                if (enemyPrefab == null)
                {
                    Debug.LogError("Enemy prefab is NULL! Skipping enemy.");
                    continue;
                }

                GameObject enemyGO = Instantiate(enemyPrefab, enemyParty.transform);
                if (enemyGO == null)
                {
                    Debug.LogError("Failed to instantiate enemy prefab!");
                    continue;
                }

                // Get the `Unit` component
                Unit enemyUnit = enemyGO.GetComponent<Unit>();
                if (enemyUnit == null)
                {
                    Debug.LogError("Instantiated enemy is missing a Unit component!");
                    continue;
                }

                // Assign enemy to party
                enemyParty.party.Add(enemyUnit);
                enemyUnit.transform.position = enemyParty.partyPos[i].position;
            }

            enemyParty.Initialize(); // Ensure enemy party is properly set up
        }


    }

}