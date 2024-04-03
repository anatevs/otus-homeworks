using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileProvider : MovableProvider
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.SetComponent<ProjectileFlag>(new ProjectileFlag());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<UniversalProvider>(out var provider))
        {
            Entity other = provider.Entity;

            if (Entity.GetComponent<Team>().value != other.GetComponent<Team>().value
                && other.Has<Health>())
            {
                if (other.Has<HealthChangeRequest>())
                {
                    other.GetComponent<HealthChangeRequest>().value +=
                        Entity.GetComponent<Damage>().value;
                }
                else
                {
                    other.AddComponent<HealthChangeRequest>().value =
                        Entity.GetComponent<Damage>().value;
                }

                Entity.AddComponent<UnspawnRequest>();
            }
        }
    }
}