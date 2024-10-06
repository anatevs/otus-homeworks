using Scripts.Chest;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveLoadNamespace
{
    public class SaveLoadChests : SaveLoad<ChestsData>
    {
        protected override string SaveLoadKey => "SaveLoadChests";

        private readonly GroupChestsConfig _config;

        public SaveLoadChests(GroupChestsConfig config)
        {
            _config = config;
        }

        protected override ChestsData LoadDefaultData()
        {
            Dictionary<string, ChestParams> dataDict = new();

            foreach (var chest in _config.Configs)
            {
                var defaultParams = chest.Params;
                defaultParams.RemainingTime = defaultParams.AwaitingTime;

                dataDict.Add(chest.Params.ChestID, defaultParams);
            }

            var data = new ChestsData();
            data.SetupChestsData(dataDict);

            return data;
        }

        public void SetChestData(string id, ChestParams chest)
        {
            _data.AddChest(id, chest);
        }
    }
}