using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

        

        public int enemyPartySize;
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

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