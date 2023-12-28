using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterComponents
    {
        readonly GameObject _character;

        public CharacterComponents(GameObject character)
        {
            _character = character;
        }
        public T GetComponent<T>()
        {
            if (_character.TryGetComponent<T>(out T resComponent))
            {
                return resComponent;
            }
            else throw new Exception("There is no component of type "+ typeof(T) + " in the " + _character + " game object");
        }
    }
}