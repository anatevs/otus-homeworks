using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEntity : Entity_Plain
{
    private Zombie _zombie;

    public void Inint(Zombie zombie)
    {
        _zombie = zombie;

        AddComponentToEntity(new DeathComponent(_zombie.isDead));
    }
}