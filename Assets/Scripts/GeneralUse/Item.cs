using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusArcadia.Assets.Scripts.GeneralUse
{
    [Serializable]
    public abstract class Item
    {
        public enum ItemType{
            Consumable,
            Equipment,
            KeyItem
        }

        public int id; // ID of Item
        public string name; // Name of Item
        public string description; // Description of Item
        public ItemType itemType;
    }

    [Serializable]
    public class Consumable : Item{
        public float healthHealed;
        public float manaHealed;

    }

    [Serializable]
    public class Equipment : Item{
        public float attackBonus;
        public float physicalDefenseBonus;
        public float magicalDefenseBonus;
    }

    [Serializable]
    public class KeyItem : Item
    {
        // Key items might not have additional fields but can be extended if needed
    }
}