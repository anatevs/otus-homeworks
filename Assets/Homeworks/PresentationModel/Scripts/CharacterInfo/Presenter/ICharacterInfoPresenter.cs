using System;

namespace Lessons.Architecture.PM
{
    public interface ICharacterInfoPresenter : IPresenter
    {
        public event Action<string, int> OnCharacterStatAdd;

        public event Action<string> OnCharacterStatRemove;

        public event Action<string, int> OnCharacterStatChanged;

        public CharacterInfo CharacterInfo { get; }

        public void AssingCharacterStats();
    }
}
