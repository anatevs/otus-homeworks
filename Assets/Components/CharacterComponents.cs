using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterComponents
    {
        public MoveComponent MoveComponent { get; private set; }

        public WeaponComponent WeaponComponent { get; private set; }

        public HitPointsComponent HitPointsComponent { get; private set; }

        private readonly GameObject _character;

        public CharacterComponents(GameObject character)
        {
            _character = character;
            MoveComponent = _character.GetComponent<MoveComponent>();
            WeaponComponent = _character.GetComponent<WeaponComponent>();
            HitPointsComponent = _character.GetComponent<HitPointsComponent>();
        }
    }
}