using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using UnityEngine.UI;

namespace MusArcadia.Assets.Scripts.UI
{
    
    public class SpellButton : MonoBehaviour {
        
        public Magic spell;

        public Text spellName;
        public Text manaCost;

        public void Start(){
            if(spell == null){
                gameObject.SetActive(false);
            }
            else{
                UpdateButton();
            }
        }

        public void UpdateButton(){
            gameObject.SetActive(true);
            spellName.text = spell.name;
            manaCost.text = "MP: " + spell.manaCost.ToString();
        }
    }
}