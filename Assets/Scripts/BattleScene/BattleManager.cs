using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    public class BattleManager : MonoBehaviour
    {
        public bool PlayerTurn;

        public bool victory;
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

        public List<PlayerPartyMember> playerParty{ get; set; }
        public List<EnemyPartyMember> [] enemyParty{ get; set; }

        private List<Entity> currentPartyTurn;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            
        }

        public void OnBattleStart(bool playerFirst)
        {

        }

        public void OnTurnStart(Entity person)
        {

        }

        public void OnTurnEnd(Entity person)
        {

        }

        public void OnBattleEnd(bool battleWon)
        {

        }


    }

}