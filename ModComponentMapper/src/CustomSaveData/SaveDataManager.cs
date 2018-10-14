using System;

namespace ModComponentMapper.SaveData
{
    internal class SaveDataManager
    {
        internal static string DATA_FILENAME_SUFFIX = "/ModComponent/CustomSaveData";

        private static SaveData saveData = new SaveData();

        public static void Clear()
        {
            saveData.Clear();
        }

        public static void Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                saveData = new SaveData();
            }
            else
            {
                saveData = Utils.DeserializeObject<SaveData>(data);
            }
        }

        public static string Serialize()
        {
            return Utils.SerializeObject(saveData);
        }

        public static string GetSaveData(int itemId, Type itemType)
        {
            return saveData.GetSaveData(itemId, itemType);
        }

        public static void SetSaveData(int itemId, Type itemType, string data)
        {
            saveData.SetSaveData(itemId, itemType, data);
        }
    }
}