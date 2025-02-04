using UnityEngine;
using UnityEngine.UI;
using MusArcadia.Assets.Scripts.BattleScene;
using MusArcadia.Assets.Scripts.GeneralUse;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MusArcadia.Assets.Scripts.UI
{
    public class BattleUI : MonoBehaviour
    {
        public int turns;
        public int halfTurns;
        public int maxTurns;

        // Battle Logic Info
        public PlayerPartyMemberInfo currentTurn;
        public List<PlayerPartyMemberInfo> partyInfo;
        public List<GameObject> partyMembers;

        // Status Panel Info
        public Text characterName;
        public Image characterSprite;
        public Text level;
        public Slider healthSlider;
        public Slider manaSlider;

        // PartyPanelInfo
        public List<Text> partyNames;

        // Party Member Images
        public List<Image> partyImages;

        // Party Member Level
        public List<Text> partyLevels;

        // Party Member Health Bars
        public List<Slider> partyHealthBars;

        // Party Member Mana Bars
        public List<Slider> partyManaBars;

        // Party Member Exp Bars
        public List<Slider> partyExpBars;


        // Party Member max health
        private float maxHealth;
        private float maxMana;

        // Party Member Max and Min Health/Mana
        private float [] PartyMaxHealth;
        private float [] PartyMaxMana;

        // Party Member HealthStatus and ManaStatus
        public List<Text> healthInfo;
        public List<Text> manaInfo;

        // Turn Panel Info
        public List<Image> Turns;
        public List<Image> HalfTurns;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (currentTurn == null)
            {
                Debug.LogError("Current turn is not assigned in BattleUI.");
                return;
            }

            setTurns();

            currentTurn.Initialize();
            foreach(var partyMember in partyInfo)
            {
                if(partyMember != null)
                {
                    partyMember.Initialize();
                }
            }
            PartyMaxHealth = new float[partyInfo.Count];
            PartyMaxMana = new float[partyInfo.Count];
            characterName.text = currentTurn.name.ToUpper();
            characterSprite.sprite = currentTurn.enitiySprite;
            level.text = "LV " + currentTurn.statSheet.level;


            
            maxHealth = currentTurn.maxHealth;
            maxMana = currentTurn.maxMana;

            healthSlider.maxValue = maxHealth;
            healthSlider.minValue = 0;

            manaSlider.maxValue = maxMana;
            manaSlider.minValue = 0;
            

            


        }

        void Update()
        {
            healthSlider.value = currentTurn.health; 
            manaSlider.value = currentTurn.mana;
        }

        public void UpdatePartyInfo()
        {
            int loc = 0;
            foreach(var partyMemberInfo in partyInfo)
            {
                if(partyMemberInfo == null)
                {
                    partyMembers[loc].gameObject.SetActive(false);
                }
                else
                {
                    // Update Graphics and Textboxes...
                    partyNames[loc].text = partyMemberInfo.name.ToUpper();
                    partyLevels[loc].text = "LV " + partyMemberInfo.statSheet.level;
                    partyImages[loc].sprite = partyMemberInfo.enitiySprite;
                    PartyMaxHealth[loc] = partyMemberInfo.maxHealth;
                    PartyMaxMana[loc] = partyMemberInfo.maxMana;
                    healthInfo[loc].text = partyMemberInfo.health + "/" + PartyMaxHealth[loc];
                    manaInfo[loc].text = partyMemberInfo.mana + "/" + PartyMaxMana[loc];

                    // Now to update the Sliders
                    partyHealthBars[loc].maxValue = PartyMaxHealth[loc];
                    partyManaBars[loc].maxValue = PartyMaxMana[loc];
                    partyExpBars[loc].maxValue = partyMemberInfo.expCap;
                    partyHealthBars[loc].minValue = 0;
                    partyManaBars[loc].minValue = 0;
                    partyExpBars[loc].minValue = 0;

                    // Now to update the Sliders values
                    partyHealthBars[loc].value = partyMemberInfo.health;
                    partyManaBars[loc].value = partyMemberInfo.mana;
                    partyExpBars[loc].value = partyMemberInfo.exp;
                }
                loc++;
            }
        }

        public void setTurns()
        {
            int loc = 0;
            foreach(var turn in Turns)
            {
                if(loc > turns)
                {
                    turn.gameObject.SetActive(false);
                }
                else
                {
                    turn.gameObject.SetActive(true);
                }
                loc++;
            }
            loc = 0;
            foreach(var halfTurn in HalfTurns)
            {
                if(loc > halfTurns)
                {
                    halfTurn.gameObject.SetActive(false);
                }
                else
                {
                    halfTurn.gameObject.SetActive(true);
                }
                loc++;
            }
        }



    }


}
