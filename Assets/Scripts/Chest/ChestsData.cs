using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Chest
{
    public sealed class ChestsData
    {
        [JsonProperty]
        private readonly Dictionary<string, ChestParams> _chestsDict = new();

        public void SetupChestsData(Dictionary<string, ChestParams> chestsDict)
        {
            _chestsDict.Clear();

            foreach (var chest in chestsDict)
            {
                _chestsDict.Add(chest.Key, chest.Value);
            }
        }

        public void SetupChestsData(ChestsData chestsData)
        {
            _chestsDict.Clear();

            var ids = chestsData.GetChestsID();

            foreach (var id in ids)
            {
                AddChest(id, chestsData.GetChestParams(id));
            }
        }

        public void AddChest(string id, ChestParams chest)
        {
            if (_chestsDict.ContainsKey(id))
            {
                _chestsDict[id] = chest;
            }
            else
            {
                _chestsDict.Add(id, chest);
            }
        }

        public ChestParams GetChestParams(string id)
        {
            return _chestsDict[id];
        }

        public string[] GetChestsID()
        {
            var ids = new string[_chestsDict.Count];

            int i = 0;
            foreach(var chest in _chestsDict.Keys)
            {
                ids[i] = chest;
                i++;
            }

            return ids;
        }
    }
}