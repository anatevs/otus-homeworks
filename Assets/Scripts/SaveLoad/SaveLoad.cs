using Newtonsoft.Json;
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

            SetupLoadData(_data);
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
            SetupSaveData(data);

            var jsonData = JsonConvert.SerializeObject(data);

            PlayerPrefs.SetString(SaveLoadKey, jsonData);
        }

        protected abstract TData LoadDefaultData();

        protected virtual void SetupLoadData(TData data) { }

        protected virtual void SetupSaveData(TData data) { }
    }
}