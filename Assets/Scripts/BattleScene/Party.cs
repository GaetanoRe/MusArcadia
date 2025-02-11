using System.Collections.Generic;
using UnityEngine;
using MusArcadia.Assets.Scripts.BattleScene;

public class Party : MonoBehaviour
{
    public List<Unit> party = new List<Unit>(); // Stores Unit GameObjects
    public List<Transform> partyPos = new List<Transform>(); // Positions for party members
    public List<Entity> partyInfo = new List<Entity>(); // Stores Entity data

    public void Initialize()
    {
        int i = 0;
        foreach(Unit unit in party)
        {
            if (partyPos[i] != null)
            {
                unit.playerTransform.position = partyPos[i].position;
            }
           
        }
    }
}
