using System;

namespace ResourcesStorage
{
    [Serializable]
    public struct ResourceInfo
    {
        public string ID;

        public int Capacity;

        public int Count;
    }
}