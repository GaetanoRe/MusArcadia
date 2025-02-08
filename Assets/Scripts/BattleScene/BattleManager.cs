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

    public class BattleManager : MonoBehaviour
    {
        public BattleState state;
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

        public BattleUI battleUI;
        public EffectsManager effectsManager;

        public List<EnemyPartyMemberInfo> enemyParty;
        public List<PlayerPartyMemberInfo> playerParty;

        public List<Entity> goesFirst;

        private List<Entity> currentPartyTurn;

        private void Start()
        {
            state = BattleState.Start;
            currentPartyTurn = goesFirst;
            SetupBattle();
        }

        void SetupBattle(){
            
        }

    }

}