using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.UI;
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
    }

    [CreateAssetMenu(fileName = "New Consumable", menuName = "Item/Consumable")]
    public class Consumable : Item{
        public float healthHealed; 
        public float manaHealed;
        public int stack;

    }

    
    public class Equipment : Item{
        
    }

    [CreateAssetMenu(fileName = "New Key Item", menuName = "Item/Key Item")]
    public class KeyItem : Item
    {
        // Key items might not have additional fields but can be extended if needed
    }

    [CreateAssetMenu(fileName = "New Armor", menuName = "Item/Equipment/Armor")]
    public class Armor : Equipment{
        public enum ArmorType
        {
            Helmet,
            Chestplate,
            Leggings,
            Boots
        }
        public List<Magic.ElementType> elementDef;
        public float minPhysDefenseBonus;
        public float maxPhysDefenseBonus;
        public float minElementDefenseBonus;
        public float maxElementDefenseBonus;
        public ArmorType armorType;
    }

   
    public class Weapon : Equipment{
        public List<Magic.ElementType> elementDam;
        public float minPhysDamageBonus;
        public float maxPhysDamageBonus;
        public float maxElementDamage;
        public float minElementDamage;
    }

    [CreateAssetMenu(fileName = "New Physical Weapon", menuName = "Item/Equipment/Weapon/Physical Weapon")]
    public class PhysicalWeapon : Weapon
    {
        public enum DamageType
        {
            Slash,
            Bash,
            Peirce
        }
        public DamageType damageType;
    }

    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Item/Equipment/Weapon/Ranged Weapon")]
    public class RangedWeapon : Weapon
    {
        public int maxRange;
        public int minRange;
    }
}