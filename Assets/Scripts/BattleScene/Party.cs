using System.Collections.Generic;
using UnityEngine;
using MusArcadia.Assets.Scripts.BattleScene;

public class Party : MonoBehaviour
{
    public new string name;
    public List<Unit> party = new List<Unit>(); // Stores Unit GameObjects
    public List<Transform> partyPos = new List<Transform>(); // Positions for party members
    public List<Entity> partyInfo = new List<Entity>(); // Stores Entity data
    public void Initialize()
    {
        if (party == null || party.Count == 0)
        {
            Debug.LogError("Party is empty or uninitialized!");
            return;
        }

        if (partyPos == null)
        {
            Debug.LogWarning("partyPos was NULL. Initializing it now.");
            partyPos = new List<Transform>(); // Ensure it's not null
        }

        foreach (Unit unit in party)
        {
            if (unit == null)
            {
                Debug.LogError("A Unit in party is NULL! Skipping.");
                continue;
            }

            partyPos.Add(unit.transform);
            partyInfo.Add(unit.playerInfo);
        }
        Debug.Log($"Party {name} Initialized! party Size: {party.Count}, partyPos Size: {partyPos.Count}, partyInfo Size: {partyInfo.Count}");
        foreach(Unit unit in party){
            Debug.Log($"{unit.name} is in the Party!");
        }

        foreach(Transform pos in partyPos){
            Debug.Log($"Position: {pos}");
        }

        foreach(Entity entity in partyInfo){
            Debug.Log($"This is the stat for {entity.name}");
        }
    }
}
