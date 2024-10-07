using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Scripts.SaveLoadNamespace
{
    public sealed class AppQuitManager : MonoBehaviour
    {
        private List<ISaveLoad> _saveLoads = new();

        [Inject]
        public void Construct(IEnumerable<ISaveLoad> saveLoads)
        {
            _saveLoads = saveLoads.ToList();
        }

        public void AddSaveLoad(ISaveLoad saveLoad)
        {
            _saveLoads.Add(saveLoad);
        }

        private void OnApplicationQuit()
        {
            if (_saveLoads.Count != 0)
            {
                foreach (var saveLoad in _saveLoads)
                {
                    saveLoad.Save();
                }
            }

            else
            {
                Debug.Log("empty saveloads");
            }
        }
    }
}