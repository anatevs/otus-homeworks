using Newtonsoft.Json;
using System.Collections.Generic;

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
    }
}