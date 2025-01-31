using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    public enum BattleState{
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    public class BattleManager : MonoBehaviour
    {
        public BattleState state;
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

        public List<Transform> playerPartyMemberPos;
        public List<Transform> enemyPartyMemberPos;
        

        public List<GameObject> playerPartyMembers{ get; set; }
        public List<GameObject> enemyPartyMembers{ get; set; }

        private List<Entity> currentPartyTurn;

        private void Start()
        {
            state = BattleState.BattleStart;
            SetupBattle();
        }

        void SetupBattle(){

        }

    }

}