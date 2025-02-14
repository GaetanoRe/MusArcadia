using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using MusArcadia.Assets.Scripts.GeneralUse;
using UnityEngine.UI;

namespace MusArcadia.Assets.Scripts.UI
{
    
    public class ItemButton : MonoBehaviour {
        
        public Consumable item;

        public Text itemName;
        public Text numberOf;

        public void Start(){
            if(item == null){
                gameObject.SetActive(false);
            }
            else{
                UpdateButton();
            }
        }

        public void UpdateButton(){
            gameObject.SetActive(true);
            itemName.text = item.name;
            numberOf.text = "No. " + item.stack;
        }
    }
}