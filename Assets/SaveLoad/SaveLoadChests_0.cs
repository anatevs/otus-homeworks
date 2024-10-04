using Newtonsoft.Json;
using Scripts.Chest;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveLoadNamespace
{
    public sealed class SaveLoadChests_0 : ISaveLoad
    {
        private const string SAVE_LOAD_KEY = "SaveLoadChests";

        private readonly GroupChestsConfig _config;

        private ChestsData _chestsData = new();

        public SaveLoadChests_0(GroupChestsConfig config)
        {
            _config = config;
        }

        public void Load()
        {
            var data = LoadChests();
            _chestsData = data;
        }

        public void Save()
        {
            SaveChests(_chestsData);
        }

        public ChestsData GetChestData()
        {
            return _chestsData;
        }

        public void SetChestData(string id, ChestParams chest)
        {
            _chestsData.AddChest(id, chest);
        }

        private ChestsData LoadChests()
        {
            if (PlayerPrefs.HasKey(SAVE_LOAD_KEY))
            {
                var jsonData = PlayerPrefs.GetString(SAVE_LOAD_KEY);

                var data = JsonConvert.DeserializeObject<ChestsData>(jsonData);

                if (data != null)
                {
                    return data;
                }
            }

            return LoadDefaultChests();
        }

        private void SaveChests(ChestsData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            Debug.Log($"data to save: {jsonData}");

            //PlayerPrefs.SetString(SAVE_LOAD_KEY, jsonData);
        }

        private ChestsData LoadDefaultChests()
        {
            Dictionary<string, ChestParams> dataDict = new();

            foreach (var chest in _config.Configs)
            {
                dataDict.Add(chest.Params.ChestID, chest.Params);
            }

            var data = new ChestsData();
            data.SetupChestsData(dataDict);

            return data;
        }
    }
}