using Newtonsoft.Json;
using UnityEngine;

namespace Assets.SaveLoad
{
    public abstract class SaveLoad<TData>
    {
        protected string SaveLoadKey { get; }

        public TData Load()
        {
            if (PlayerPrefs.HasKey(SaveLoadKey))
            {
                return LoadFromSaved();
            }

            return LoadDefault();
        }

        public void Save(TData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            PlayerPrefs.SetString(SaveLoadKey, jsonData);
        }

        protected abstract TData LoadFromSaved();

        protected abstract TData LoadDefault();
    }
}