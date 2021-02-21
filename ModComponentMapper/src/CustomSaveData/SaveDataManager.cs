using System;
using MelonLoader.TinyJSON;

//did a first pass through; has a conversion issue which I think I fixed
//does not need to be declared

namespace ModComponentMapper.SaveData
{
    public struct SaveProxy
    {
        public string data;
    }

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
                SaveProxy saveProxy = JSON.Load(data).Make<SaveProxy>();

                saveData = JSON.Load(saveProxy.data).Make<SaveData>();
                
                //saveData = MelonLoader.TinyJSON.JSON.Load(data).Make<SaveData>();
                //saveData = Utils.DeserializeObject<SaveData>(data);
            }
        }

        public static string Serialize()
        {
            SaveProxy saveProxy = new SaveProxy
            {
                data = JSON.Dump(saveData)
            };

            return JSON.Dump(saveProxy);

            //return MelonLoader.TinyJSON.JSON.Dump(saveData);
            //return Utils.SerializeObject(saveData); //<==============================================================
        }

        //fixed (I think) serialization issue by making SaveData inherit from Il2CppSystem.Object in SaveData.cs

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