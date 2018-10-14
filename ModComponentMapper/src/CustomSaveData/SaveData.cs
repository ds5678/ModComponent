using System;
using System.Collections.Generic;

namespace ModComponentMapper.SaveData
{
    public class SaveData
    {
        private Dictionary<string, string> itemSaveData = new Dictionary<string, string>();

        public void Clear()
        {
            this.itemSaveData.Clear();
        }

        public string GetSaveData(int itemId, Type itemType)
        {
            this.itemSaveData.TryGetValue(GetKey(itemId, itemType), out string data);
            return data;
        }

        public void SetSaveData(int itemId, Type itemType, string data)
        {
            this.itemSaveData[GetKey(itemId, itemType)] = data;
        }

        private static string GetKey(int itemId, Type itemType)
        {
            return itemId + "_" + itemType.Name;
        }
    }
}