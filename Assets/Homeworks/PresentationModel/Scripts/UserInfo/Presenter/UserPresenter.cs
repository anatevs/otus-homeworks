using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserPresenter : IUserPresenter
    {
        public event Action OnUserinfoChanged;

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Sprite Icon { get; private set; }

        
        private UserInfo _userInfo;


        public UserPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;
            
            Name = _userInfo.Name;
            Description = _userInfo.Description;
            Icon = _userInfo.Icon;

            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriprtionChanged;
            _userInfo.OnIconChanged += OnIconChanged;
        }

        public void OnNameChanged(string name)
        {
            Name = name;
            OnUserinfoChanged?.Invoke();
        }

        public void OnDescriprtionChanged(string description)
        {
            Description = description;
            OnUserinfoChanged?.Invoke();
        }

        public void OnIconChanged(Sprite icon)
        {
            Icon = icon;
            OnUserinfoChanged?.Invoke();
        }

        ~UserPresenter()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriprtionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;
        }
    }
}