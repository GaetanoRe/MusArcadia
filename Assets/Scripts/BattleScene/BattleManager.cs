using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.BattleScene
{
    public class BattleManager : MonoBehaviour
    {
        public int fullTurns { get; set; }
        public int halfTurns { get; set; }

        public PlayerPartyMember [] playerParty{ get; set; }
        public EnemyPartyMember [] enemyParty{ get; set; }

        
    }

}