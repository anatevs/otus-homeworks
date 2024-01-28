using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IStatPresenter : IPresenter, IDisposable
    {
        public event Action OnCharacterStatChanged;

        public string StatText { get; }
    }
}