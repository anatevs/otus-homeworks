using Newtonsoft.Json;
using UnityEngine;

namespace Scripts.SaveLoadNamespace
{
    public abstract class SaveLoad<TData> : ISaveLoad
    {
        protected string SaveLoadKey { get; }

        public void Save()
        {

        }

        public void Load()
        {

        }

        public TData LoadData()
        {
            if (PlayerPrefs.HasKey(SaveLoadKey))
            {
                return LoadFromSaved();
            }

            return LoadDefaultData();
        }

        public void SaveData(TData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            PlayerPrefs.SetString(SaveLoadKey, jsonData);
        }

        protected abstract TData LoadFromSaved();

        protected abstract TData LoadDefaultData();
    }
}