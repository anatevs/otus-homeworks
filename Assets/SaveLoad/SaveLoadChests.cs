using Newtonsoft.Json;
using Scripts.Chest;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveLoad
{
    public class SaveLoadChests
    {
        private const string SAVE_LOAD_KEY = "SaveLoadChests";

        private readonly GroupChestsConfig _config;

        public SaveLoadChests(GroupChestsConfig config)
        {
            _config = config;
        }

        public ChestsData Load()
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

            return LoadDefault();
        }

        public void Save(ChestsData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            PlayerPrefs.SetString(SAVE_LOAD_KEY, jsonData);
        }

        private ChestsData LoadDefault()
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