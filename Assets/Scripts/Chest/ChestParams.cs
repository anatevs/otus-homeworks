using System;

namespace Scripts.Chest
{
    [Serializable]
    public struct ChestParams
    {
        public string ChestID;

        public string RewardType;

        public string RewardAmout;

        public TimeStruct TimeToOpen;
    }
}