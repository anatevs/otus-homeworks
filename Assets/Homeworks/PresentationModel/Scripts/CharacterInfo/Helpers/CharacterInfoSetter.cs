using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfoSetter : MonoBehaviour
    {
        [SerializeField]
        private CharacterStatStruct _statForEdit;

        private CharacterInfo _characterInfo;

        private List<CharacterStatStruct> _statsList = new List<CharacterStatStruct>();

        [Inject]
        public void Construct(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
        }

        public void AddStat()
        {
            if (!_statsList.Contains(_statForEdit) && _statForEdit.name != "")
            {
                _statsList.Add(_statForEdit);
                CharacterStat statForAdd = new(_statForEdit.name, _statForEdit.value);
                _characterInfo.AddStat(statForAdd);
                _statForEdit = new CharacterStatStruct();
            }
            else
            {
                throw new Exception($"character stat {_statForEdit.name} is empty or also in the list");
            }
        }

        public void RemoveStat()
        {
            if (_statsList.Exists(s => s.name == _statForEdit.name))
            {
                _statsList.Remove(_statForEdit);
                _characterInfo.RemoveStat(_characterInfo.GetStat(_statForEdit.name));
                _statForEdit = new CharacterStatStruct();
            }
            else
            {
                throw new Exception($"character stat {_statForEdit.name} is not in stats list");
            }
        }

        public void ChangeStat()
        {
            if (_statsList.Exists(s => s.name == _statForEdit.name))
            {
                CharacterStatStruct changeStatStruct = _statsList[_statsList.FindIndex(s => s.name == _statForEdit.name)];
                changeStatStruct.value = _statForEdit.value;
                _characterInfo.ChangeStat(_statForEdit.name, _statForEdit.value);
                _statForEdit = new CharacterStatStruct();
            }
            else
            {
                throw new Exception($"character stat {_statForEdit.name} is not in stats list");
            }
        }
    }
}