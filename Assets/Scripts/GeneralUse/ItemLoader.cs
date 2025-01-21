using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusArcadia.Assets.Scripts.GeneralUse
{
   public static class ItemLoader
    {
        public static List<Consumable> LoadConsumables()
        {
            TextAsset consumablesJson = Resources.Load<TextAsset>("Databases/consumables");
            if (consumablesJson == null)
            {
                Debug.LogError("Could not find consumables.json in Resources.");
                return new List<Consumable>();
            }

            return JsonUtility.FromJson<ListWrapper<Consumable>>(consumablesJson.text).items;
        }

        public static List<Equipment> LoadEquipment()
        {
            TextAsset equipmentJson = Resources.Load<TextAsset>("Databases/equipment");
            if (equipmentJson == null)
            {
                Debug.LogError("Could not find equipment.json in Resources.");
                return new List<Equipment>();
            }

            return JsonUtility.FromJson<ListWrapper<Equipment>>(equipmentJson.text).items;
        }

        public static List<KeyItem> LoadKeyItems()
        {
            TextAsset keyItemsJson = Resources.Load<TextAsset>("Databases/key_items");
            if (keyItemsJson == null)
            {
                Debug.LogError("Could not find key_items.json in Resources.");
                return new List<KeyItem>();
            }

            return JsonUtility.FromJson<ListWrapper<KeyItem>>(keyItemsJson.text).items;
        }

        // Wrapper for JSON arrays
        [System.Serializable]
        private class ListWrapper<T>
        {
            public List<T> items;
        }
    }
}