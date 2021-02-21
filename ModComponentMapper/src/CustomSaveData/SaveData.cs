using System;
using System.Collections.Generic;
using UnhollowerBaseLib.Attributes;

//did a first pass through; didn't find anything
//MIGHT NEED to add constructor 
//declared class for MelonLoader

namespace ModComponentMapper.SaveData 
{
    public class SaveData //added inheritance to fix SaveData serialization issue
    {
        public Dictionary<string, string> itemSaveData = new Dictionary<string, string>();

        public void Clear()
        {
            this.itemSaveData.Clear();
        }

        [HideFromIl2Cpp]
        public string GetSaveData(int itemId, Type itemType)
        {
            this.itemSaveData.TryGetValue(GetKey(itemId, itemType), out string data);
            return data;
        }

        [HideFromIl2Cpp]
        public void SetSaveData(int itemId, Type itemType, string data)
        {
            this.itemSaveData[GetKey(itemId, itemType)] = data;
        }

        private static string GetKey(int itemId, Type itemType)
        {
            return itemId + "_" + itemType.Name;
        }

        public SaveData() { }
    }
}