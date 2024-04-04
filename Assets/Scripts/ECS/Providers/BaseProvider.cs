using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class BaseProvider : UniversalProvider
{
    private TeamService _teamService;

    

    protected override void Initialize()
    {
        base.Initialize();

        Entity.AddComponent<Position>() = new Position() { value = transform.position };
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //finish game logic
    }
}