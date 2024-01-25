using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IUserPresenter : IPresenter
    {
        public event Action OnUserinfoChanged;

        public string Name { get; }

        public string Description { get; }

        public Sprite Icon { get; }
    }
}
