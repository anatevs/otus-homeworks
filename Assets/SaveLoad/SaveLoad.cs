using Newtonsoft.Json;
using Scripts.Chest;
using UnityEngine;

namespace Scripts.SaveLoadNamespace
{
    public abstract class SaveLoad<TData> : ISaveLoad
    {
        protected abstract string SaveLoadKey { get; }

        protected TData _data;

        public TData GetData()
        {
            return _data;
        }

        public void Save()
        {
            SaveData(_data);
        }

        public void Load()
        {
            _data = LoadData();

            SetupData(_data);
        }

        public TData LoadData()
        {
            if (PlayerPrefs.HasKey(SaveLoadKey))
            {
                var jsonData = PlayerPrefs.GetString(SaveLoadKey);

                var data = JsonConvert.DeserializeObject<TData>(jsonData);

                return data;
            }

            return LoadDefaultData();
        }

        public void SaveData(TData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            Debug.Log($"data to save: {jsonData}");

            //PlayerPrefs.SetString(SaveLoadKey, jsonData);
        }

        protected abstract TData LoadDefaultData();

        protected virtual void SetupData(TData data) { }
    }
}