using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECSAdmin : MonoBehaviour
{
    private World _world;
    private SystemsGroup _systemsGroup;

    private void Awake()
    {
        _world = World.Default;


        _systemsGroup = _world.CreateSystemsGroup();

        
        _systemsGroup.AddSystem(new HealthSystem(_world));


        _world.AddSystemsGroup(order: 0, _systemsGroup);
    }

    private void Update()
    {
        //_world.Update(Time.deltaTime);
    }
}