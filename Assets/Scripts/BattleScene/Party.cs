using System.Collections.Generic;
using UnityEngine;
using MusArcadia.Assets.Scripts.BattleScene;

public class Party : MonoBehaviour
{
    public List<GameObject> party; // GameObjects for party members
    public List<Transform> partyPos; // Positions for party members
    public List<Entity> partyInfo; // The actual party data

    public void Initialize()
    {
        // Ensure the party list has the correct size
        while (party.Count > partyInfo.Count)
        {
            party.RemoveAt(party.Count - 1);
        }
        while (party.Count < partyInfo.Count)
        {
            party.Add(null);
        }

        // Iterate based on the size of partyInfo
        for (int i = 0; i < partyInfo.Count; i++)
        {
            if (partyInfo[i] != null)
            {
                if (party[i] == null)
                {
                    // Instantiate or find the GameObject if it doesn't exist
                    GameObject newPartyMember = Instantiate(Resources.Load<GameObject>(partyInfo[i].name));
                    newPartyMember.transform.position = partyPos[i].position;
                    party[i] = newPartyMember;
                }

                // Ensure the GameObject is active and set up
                party[i].SetActive(true);
            }
            else
            {
                // If there's no party info, remove the GameObject reference
                if (party[i] != null)
                {
                    Destroy(party[i]); // Clean up the object if needed
                    party[i] = null;
                }
            }
        }
    }
}
