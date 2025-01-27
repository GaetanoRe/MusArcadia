using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MusArcadia.Assets.Scripts.GeneralUse
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/GenericItem")]
    public abstract class Item : ScriptableObject 
    {
        public enum ItemType{
            Consumable,
            Equipment,
            KeyItem
        }

        public int id; // ID of Item
        public new string name; // Name of Item
        public string description; // Description of Item

        public Sprite artwork; // Artwork for Item
        public ItemType itemType; 
    }

    [CreateAssetMenu(fileName = "New Consumable", menuName = "Item/Consumable")]
    public class Consumable : Item{
        public float healthHealed; 
        public float manaHealed;

    }

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item/Equipment")]
    public class Equipment : Item{
        public float attackBonus;
        public float physicalDefenseBonus;
        public float magicalDefenseBonus;
    }

    [CreateAssetMenu(fileName = "New Key Item", menuName = "Item/Key Item")]
    public class KeyItem : Item
    {
        // Key items might not have additional fields but can be extended if needed
    }
}