using System.Collections.Generic;
using MusArcadia.Assets.Scripts.BattleScene;
using MusArcadia.Assets.Scripts.GeneralUse;
using MusArcadia.Assets.Scripts.UI;
using UnityEngine;

public class SpellButtonContainer : MonoBehaviour
{
    public List<SpellButton> spellButtons;
    public PlayerPartyMemberInfo currentPlayerTurn;

    public BattleUI battleUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateContainer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateContainer(){
        int i = 0;
        currentPlayerTurn = battleUI.currentTurn;
        foreach (Magic spell in currentPlayerTurn.spellBook){
            spellButtons[i].spell = spell;
            spellButtons[i].UpdateButton();
        }
    }
}
